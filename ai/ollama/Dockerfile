FROM ollama/ollama:0.6.9-rc0

RUN ollama pull llama3.2:1b
RUN ollama pull mxbai-embed-large

CMD ["ollama", "serve"]