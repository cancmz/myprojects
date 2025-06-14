import pandas as pd
import matplotlib.pyplot as plt
import os
from matplotlib.ticker import ScalarFormatter

csv_path = "simulation_output.csv"
df = pd.read_csv(csv_path)

sun_df = df[df["Body"] == "Sun"].reset_index(drop=True)
planet_names = [name for name in df["Body"].unique() if name != "Sun"]

output_folder = "planet_distance_graphs"
os.makedirs(output_folder, exist_ok=True)

plt.rcParams.update({
    "font.family": "sans-serif",
    "font.size": 14,
    "axes.titlesize": 16,
    "axes.labelsize": 14,
    "xtick.labelsize": 12,
    "ytick.labelsize": 12
})

for planet in planet_names:
    planet_df = df[df["Body"] == planet].reset_index(drop=True)
    distances = ((planet_df["PosX"] - sun_df["PosX"]) ** 2 +
                 (planet_df["PosY"] - sun_df["PosY"]) ** 2 +
                 (planet_df["PosZ"] - sun_df["PosZ"]) ** 2) ** 0.5

    min_d = distances.min()
    max_d = distances.max()

    padding = (max_d - min_d) * 0.01
    y_min = min_d - padding
    y_max = max_d + padding

    plt.figure(figsize=(12, 6), dpi=300)
    plt.plot(planet_df["Day"], distances, linewidth=2)
    plt.title(f"{planet} - Distance to the Sun Over Time")
    plt.xlabel("Day")
    plt.ylabel("Distance (m)")
    plt.grid(True, linestyle="--", alpha=0.7)

    ax = plt.gca()
    ax.ticklabel_format(style='sci', axis='y', scilimits=(11, 11))
    ax.set_ylim(y_min, y_max)

    plt.tight_layout()
    plt.savefig(f"{output_folder}/{planet}_distance_to_sun.png", dpi=300)
    plt.close()

print("Graphs saved successfully.")
