import express from "express";
import cors from "cors";
import { createOllama } from "ollama-ai-provider";
import { embed, generateObject, generateText, Message } from "ai";
import pg from "pg";
import { sentimentSchema } from "./schemas.js";
import { config } from "dotenv";

config();

const db = new pg.Client({
  host: process.env.DB_HOST,
  port: parseInt(process.env.DB_PORT!),
  database: process.env.DB_DATABASE,
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
});

db.connect();

const ollama = createOllama();

const model = ollama("llama3.2:1b");
const embeddings = ollama.embedding("mxbai-embed-large");

const app = express();

app.use(
  cors({
    origin: "*",
    methods: ["POST"],
  })
);
app.use(express.json());

app.post("/embed", async (req: express.Request, res: express.Response) => {
  const recipe = req.body.message;

  const embeddingRes = await embed({
    model: embeddings,
    value: recipe,
  });

  res.send({
    embedding: embeddingRes.embedding,
  });
});

app.post("/rag", async (req: express.Request, res: express.Response) => {
  const recipe = req.body.message;

  console.log(recipe);

  const embeddingRes = await embed({
    model: embeddings,
    value: recipe,
  });

  try {
    // const vector = embeddingRes.embedding
    //   .toString()
    //   .replace("{", "")
    //   .replace("}", "")
    //   .split(",")
    //   .map((x) => parseFloat(x));

    const queryRes = await db.query(
      `SELECT "Recipe", "RecipeId" FROM "RecipesVectors" WHERE "Recipe" IS NOT NULL AND "Recipe" != '' LIMIT 1`,
      []
    );

    const messages: Message[] = [
      {
        id: "1",
        role: "system",
        content:
          "Jesteś pomocnym asystentem kucharza-amatora, podpowiadaj tylko na bazie tego co wiesz o przepisach z bazy - zwróć pasujący RecipeId",
      },
    ];

    for (const r of queryRes.rows) {
      messages.push({
        id: r.RecipeId,
        role: "system",
        content: `RecipeId: ${r.RecipeId}, przepis : ${r.Recipe}`,
      });
    }

    messages.push({
      id: "user-message",
      role: "user",
      content: recipe,
    });

    const modelRes = await generateText({
      model,
      messages,
    });

    res.send(modelRes);
  } catch (err) {
    console.error(err);

    res.send(":(");
  }
});

app.post("/sentiment", async (req: express.Request, res: express.Response) => {
  const recipe = req.body.message;
  const prompt = `Przeanalizuj poziom trudności przepisu: ${recipe}`;

  const modelRes = await generateObject({
    model,
    schema: sentimentSchema,
    prompt,
    system:
      "Jesteś pomocnikiem do określania poziomu trudności przepisu kulinarnego.",
  });

  res.send(modelRes.object);
});

app.listen(process.env.PORT, () => {
  console.log("Running AI server");
});
