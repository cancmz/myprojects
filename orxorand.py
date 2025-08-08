import numpy as np

X = np.array([
    [0, 0, 0],
    [0, 1, 0],
    [1, 0, 0],
    [1, 1, 0],
    [0, 0, 1],
    [0, 1, 1],
    [1, 0, 1],
    [1, 1, 1],
    [0, 0, 2],
    [0, 1, 2],
    [1, 0, 2],
    [1, 1, 2]
], dtype=np.float32)

Y = np.array([
    [0],
    [0],
    [0],
    [1],  # AND

    [0],
    [1],
    [1],
    [1],  # OR

    [0],
    [1],
    [1],
    [0]  # XOR
], dtype=np.float32)

X[:, 2] = X[:, 2] / 2.0

np.random.seed(42)
input_size = 3
hidden_size = 6
output_size = 1
lr = 0.1
epochs = 100000

W1 = np.random.randn(input_size, hidden_size)
b1 = np.zeros((1, hidden_size))
W2 = np.random.randn(hidden_size, output_size)
b2 = np.zeros((1, output_size))


def sigmoid(x):
    return 1 / (1 + np.exp(-x))


def sigmoid_derivative(x):
    s = sigmoid(x)
    return s * (1 - s)

for epoch in range(epochs):
    Z1 = X @ W1 + b1
    A1 = sigmoid(Z1)
    Z2 = A1 @ W2 + b2
    A2 = sigmoid(Z2)

    loss = np.mean((A2 - Y) ** 2)

    dA2 = 2 * (A2 - Y)
    dZ2 = dA2 * sigmoid_derivative(Z2)
    dW2 = A1.T @ dZ2
    db2 = np.sum(dZ2, axis=0, keepdims=True)

    dA1 = dZ2 @ W2.T
    dZ1 = dA1 * sigmoid_derivative(Z1)
    dW1 = X.T @ dZ1
    db1 = np.sum(dZ1, axis=0, keepdims=True)

    W2 -= lr * dW2
    b2 -= lr * db2
    W1 -= lr * dW1
    b1 -= lr * db1

    if epoch % 100 == 0:
        print(f"Epoch {epoch}, Loss: {loss:.4f}")

def interactive_test():

    print("0: AND")
    print("1: OR")
    print("2: XOR")
    try:
        op = int(input("Choose an operation (0/1/2): "))
        if op not in [0, 1, 2]:
            print("Ivlaid input")
            return
        x1 = int(input("x1 (0 or 1): "))
        x2 = int(input("x2 (0 or 1): "))
        if x1 not in [0, 1] or x2 not in [0, 1]:
            print("Inputs can only be 0 or 1.")
            return
        user_guess = int(input(f"for {x1} and {x2} {['AND', 'OR', 'XOR'][op]} guess the result (0 or 1): "))
        if user_guess not in [0, 1]:
            print("The guess can only be 0 or 1.")
            return
    except:
        print("Invalid input")
        return

    X_input = np.array([[x1, x2, op / 2.0]], dtype=np.float32)
    Z1 = X_input @ W1 + b1
    A1 = sigmoid(Z1)
    Z2 = A1 @ W2 + b2
    A2 = sigmoid(Z2)
    prediction = int(np.round(A2).item())

    print(f"\nGuess of the model: {prediction}")
    if prediction == user_guess:
        print("Congrats, you guessed it!")
    else:
        print(f"Wrong guess. The answer: {prediction}")

while True:
    interactive_test()
    again = input("\nWould you like to try a new operation? (y/n): ")
    if again.lower() != 'y':
        break