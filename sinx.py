import numpy as np
import matplotlib.pyplot as plt

# 1. Giriş verisi ve gerçek sin(x) çıktısı
x = np.linspace(0, 2 * np.pi, 100).reshape(-1, 1)  # shape: (100, 1)
y_true = np.sin(x)  # shape: (100, 1)

# 2. Ağ yapısı
input_size = 1
hidden_size = 10
output_size = 1

np.random.seed(42)
W1 = np.random.randn(input_size, hidden_size) * 0.1  # (1, 10)
b1 = np.zeros((1, hidden_size))  # (1, 10)
W2 = np.random.randn(hidden_size, output_size) * 0.1  # (10, 1)
b2 = np.zeros((1, output_size))  # (1, 1)


# 3. Aktivasyon fonksiyonu ve türevi (tanh)
def tanh(x):
    return np.tanh(x)


def tanh_derivative(x):
    return 1 - np.tanh(x) ** 2


# 4. Eğitim ayarları
lr = 0.05
epochs = 100000
losses = []

# 5. Eğitim döngüsü
for epoch in range(epochs):
    # İleri yayılım
    z1 = x @ W1 + b1  # (100, 10)
    a1 = tanh(z1)  # (100, 10)
    z2 = a1 @ W2 + b2  # (100, 1)
    y_pred = z2  # linear aktivasyon

    # Kayıp (Loss) hesapla
    loss = np.mean((y_pred - y_true) ** 2)
    losses.append(loss)

    # Geri yayılım (Backpropagation)
    dL_dy = 2 * (y_pred - y_true) / y_true.shape[0]  # (100, 1)

    dL_dW2 = a1.T @ dL_dy  # (10, 1)
    dL_db2 = np.sum(dL_dy, axis=0, keepdims=True)  # (1, 1)

    dL_da1 = dL_dy @ W2.T  # (100, 10)
    dL_dz1 = dL_da1 * tanh_derivative(z1)  # (100, 10)

    dL_dW1 = x.T @ dL_dz1  # (1, 10)
    dL_db1 = np.sum(dL_dz1, axis=0, keepdims=True)  # (1, 10)

    # Ağırlık güncelle
    W2 -= lr * dL_dW2
    b2 -= lr * dL_db2
    W1 -= lr * dL_dW1
    b1 -= lr * dL_db1

# Eğitim sonrası sonucu çiz
plt.figure(figsize=(10, 4))
plt.subplot(1, 2, 1)
plt.plot(x, y_true, label="Gerçek sin(x)")
plt.plot(x, y_pred, label="Tahmin", linestyle="--")
plt.title("Tahmin Sonucu")
plt.xlabel("x")
plt.ylabel("y")
plt.legend()
plt.grid(True)

# Kayıp grafiği
plt.subplot(1, 2, 2)
plt.plot(losses)
plt.title("Eğitim Süreci - Loss")
plt.xlabel("Epoch")
plt.ylabel("MSE Loss")
plt.grid(True)
plt.tight_layout()
plt.show()
