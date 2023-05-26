import json
import jsonlines
import sys


def corpus2dataset(corpus_path, dataset_path):
    print("Loading corpus...")
    with open(corpus_path, encoding='utf-8') as f:
        corpus = json.load(f)
    with open(dataset_path, mode='w', encoding='utf-8') as writer:
        print("Creating dataset...")
        libros = corpus["libros"]
        for libro in libros:
            texto = libro['document']
            # split the texto string by dots and store it in a list. then for each phrase, create a dict where the first half is the key and the second half is the value            
            for frase in texto.split('.'):
                firstpart, secondpart = frase[:len(frase)//2], frase[len(frase)//2:]
                if (len(firstpart) > 1) & (len(secondpart) > 1):
                    if(firstpart[0] != ' '):
                        firstpart = ' ' + firstpart
                    if(secondpart[0] != ' '):
                        secondpart = ' ' + secondpart
                    dataset = {}
                    dataset['prompt'] = firstpart + '\n\n###\n\n'
                    dataset['completion'] = secondpart + '###'
                    writer.write(json.dumps(dataset, ensure_ascii=False) + '\n')
    f.close()

if __name__ == "__main__":
    corpus_path = sys.argv[1]  # e.g. corpus_es.json
    dataset_path = sys.argv[2]  # e.g. dataset_es.json
    corpus2dataset(corpus_path, dataset_path) 

