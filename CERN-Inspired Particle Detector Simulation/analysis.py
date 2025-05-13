def run_analysis():
    import matplotlib.pyplot as plt
    import os
    from datetime import datetime
    import random

    base_dir = os.path.dirname(os.path.abspath(__file__))
    results_dir = os.path.join(base_dir, "results")
    if not os.path.exists(results_dir):
        os.makedirs(results_dir)

    seconds = list(range(60))
    incoming = [random.randint(80, 120) for _ in seconds]
    hits = [int(i * random.uniform(0.08, 0.12)) for i in incoming]

    plt.figure(figsize=(10, 5), dpi=300)
    plt.plot(seconds, hits, label="Hits", linewidth=2, marker='o', markersize=4)
    plt.plot(seconds, incoming, label="Incoming", linewidth=2, marker='o', markersize=4)

    plt.xticks(ticks=range(0, 61, 5), fontsize=10)
    max_y = max(incoming + hits)
    plt.yticks(ticks=range(0, max_y + 10, 10), fontsize=10)

    plt.xlabel("Time (seconds)", fontsize=11)
    plt.ylabel("Particle Count", fontsize=11)
    plt.title("Detected vs Incoming Particles Over Time", fontsize=13)

    plt.grid(True, linestyle='--', linewidth=0.5, alpha=0.6)
    plt.legend(loc="upper right", fontsize=10)

    timestamp = datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    plot_path = os.path.join(results_dir, f"plot_{timestamp}.png")
    plt.savefig(plot_path, dpi=300, bbox_inches='tight')
    print(f"Plot saved to: {plot_path}")
    plt.show()

if __name__ == "__main__":
    run_analysis()
