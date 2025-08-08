import os

# Proje klasör yapısını oluşturma
base_dir = "/mnt/data/custom-image-classifier"

folders = [
    "data/train/class1",
    "data/train/class2",
    "data/test/class1",
    "data/test/class2"
]

files = [
    "train.py",
    "predict.py",
    "requirements.txt",
    "README.md",
    "LICENSE"
]

# Klasörleri oluştur
for folder in folders:
    os.makedirs(os.path.join(base_dir, folder), exist_ok=True)

# Dosyaları oluştur
for file in files:
    open(os.path.join(base_dir, file), 'w').close()

base_dir  # klasör yapısı oluşturulduğu yol

print(base_dir)