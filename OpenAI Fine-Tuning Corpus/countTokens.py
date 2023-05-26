import argparse
from transformers import GPT2TokenizerFast
import jsonlines

def tokenize_jsonl_file(jsonl_path, tokenizer):
    total_tokens = 0
    with jsonlines.open(jsonl_path) as reader:
        for line in reader:
            prompt_tokens = tokenizer.encode(line["prompt"])
            completion_tokens = tokenizer.encode(line["completion"])
            total_tokens += len(prompt_tokens) + len(completion_tokens)
    return total_tokens

# create CLI argument parser
parser = argparse.ArgumentParser(description="Tokenize a JSONL file with the GPT-2 tokenizer.")
parser.add_argument("jsonl_file", type=str, help="path to JSONL file to tokenize")

# load pre-trained tokenizer
tokenizer = GPT2TokenizerFast.from_pretrained("gpt2")

# parse command line arguments
args = parser.parse_args()

# tokenize JSONL file and get total number of tokens
jsonl_path = args.jsonl_file
total_tokens = tokenize_jsonl_file(jsonl_path, tokenizer)
print(f"Total number of tokens in {jsonl_path}: {total_tokens}")

## jsonl file total tokens: 1.545.530
