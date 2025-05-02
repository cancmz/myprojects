def run_analysis():
    import pandas as pd
    import matplotlib.pyplot as plt

    df = pd.read_csv("results/counts.csv")

    plt.figure(figsize=(12, 6), dpi=150)
    plt.plot(df["Second"], df["Hits"], label="Hits", linewidth=2, marker='o')
    plt.plot(df["Second"], df["Incoming"], label="Incoming", linewidth=2, marker='o')

    plt.xticks(ticks=range(0, 61, 5))  ,
    max_y = max(df["Incoming"].max(), df["Hits"].max())
    plt.yticks(ticks=range(0, max_y + 2))

    plt.xlabel("Time (seconds)", fontsize=12, fontweight='bold')
    plt.ylabel("Particle Count", fontsize=12, fontweight='bold')
    plt.title("Detected vs Incoming Particles Over Time", fontsize=14, fontweight='bold')

    plt.grid(True, linestyle='--', alpha=0.7)
    plt.legend(loc="upper right", fontsize=11)

    plt.savefig("results/plot.png", dpi=300, bbox_inches='tight')
    plt.show()

if __name__ == "__main__":
    run_analysis()
