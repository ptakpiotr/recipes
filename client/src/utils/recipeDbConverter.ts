import type { IRecipeCreateDtoOffline } from "../../Types";

// function getAllByPrefix(formData: FormData, prefix: string) {
//   const result = [];
//   for (const [key, value] of formData.entries()) {
//     if (key.startsWith(prefix)) {
//       result.push(value);
//     }
//   }
//   return result;
// }

export const convertRecipeToDbEntry = async (
  formData: FormData
): Promise<IRecipeCreateDtoOffline> => {
  const file = formData.get("image") as File;
  const arrayBuffer = await file.arrayBuffer();
  const uint8Array = new Uint8Array(arrayBuffer);
  //potential TODO: change ingredients logic
  return {
    title: formData.get("title")!.toString(),
    description: formData.get("description")!.toString(),
    image: Array.from(uint8Array),
    imageName: file.name,
    types: [],
    ingredients: [],
    isNew: true,
  };
};
