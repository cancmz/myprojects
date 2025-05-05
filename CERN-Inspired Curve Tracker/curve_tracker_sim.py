import random
import math
import matplotlib.pyplot as plt
import matplotlib as mpl
from datetime import datetime


class Particle:
    def __init__(self, B=1.0, momentum_range=(1.0, 5.0)):
        self.charge = random.choice([-1, 1])
        self.momentum = round(random.uniform(*momentum_range), 3)
        self.angle_deg = round(random.uniform(0, 360), 2)
        self.angle_rad = math.radians(self.angle_deg)
        self.B = B
        self.radius = round(self.momentum / (abs(self.charge) * self.B), 3)

    def get_track_points(self, numpoints=200, arc_fraction=0.5):
        direction = 1 if self.charge > 0 else -1
        theta_start = self.angle_rad
        arc_length = arc_fraction * 2 * math.pi

        xc = -direction * self.radius * math.sin(theta_start)
        yc = -direction * self.radius * math.cos(theta_start)

        x_vals = []
        y_vals = []

        for i in range(numpoints):
            t = arc_length * (i / numpoints - 1)
            theta = theta_start + direction * t

            x = xc + self.radius * math.cos(theta)
            y = yc + self.radius * math.sin(theta)
            x_vals.append(x)
            y_vals.append(y)
        return x_vals, y_vals, arc_fraction


def draw_particles(particles, B):
    plt.grid(True, linestyle='--', linewidth=0.5, alpha=0.6)
    plt.figure(figsize=(10, 10))
    plt.title("Charged Particle Tracks in Magnetic Field")
    plt.xlabel("X Position", fontsize=14)
    plt.ylabel("Y Position", fontsize=14)
    plt.grid(True, linestyle='--', linewidth=0.5, alpha=0.6)
    plt.axis('equal')

    for p in particles:
        plt.axvline(0, color='black', linestyle='-', linewidth=0.7)
        plt.axhline(0, color='black', linestyle='-', linewidth=0.7)

        x_vals, y_vals, arc = p.get_track_points()
        color = 'blue' if p.charge > 0 else 'red'
        color2 = 'red' if p.charge > 0 else 'blue'

        plt.plot(x_vals, y_vals, color=color, linewidth=2, alpha=0.8)

        mid = len(x_vals) // 2
        if mid + 1 < len(x_vals):
            dx = x_vals[mid + 1] - x_vals[mid]
            dy = y_vals[mid + 1] - y_vals[mid]
            plt.quiver(
                x_vals[mid], y_vals[mid], dx, dy, angles='xy', scale_units='xy', scale=0.15, width=0.01, color=color2,
                alpha=0.8
            )
    plt.text(
        0.02, 0.98,
        f"Magnetic Field: {B:.1f} T\nArc Angle: {arc * 360:.1f}\nEnvironment: Vacuum",
        transform=plt.gca().transAxes,
        fontsize=10, verticalalignment='top'
    )

    plt.text(
        -0.14, 1.12,
        "X and Y axes are in meters. Trajectories are based on SI units (q in C, p in kg·m/s, B in T).",
        transform=plt.gca().transAxes,
        fontsize=10,
        verticalalignment='bottom',
        horizontalalignment='left'
    )

    plt.grid(True)
    timestamp = datetime.now().strftime("%Y-%m-%d_%H-%M-%S")
    filename = f"results/plot_{timestamp}.png"

    plt.savefig(filename, dpi=600, bbox_inches='tight')
    print(f"Grafik başarıyla kaydedildi: {filename}")
    plt.show()


if __name__ == "__main__":

    NUM_PARTICLES = 15
    B_FIELD = 1.0
    MOMENTUM_RANGE = (1.0, 5.0)

    particles = []
    for i in range(NUM_PARTICLES):
        p = Particle(B=B_FIELD, momentum_range=MOMENTUM_RANGE)
        particles.append(p)

    print(f"{'ID':<3} {'Charge':<7} {'Momentum':<9} {'Angle (°)':<10} {'Radius':<7}")
    print("-" * 40)
    for i, p in enumerate(particles, start=1):
        print(f"{i:<3} {p.charge:<7} {p.momentum:<9} {p.angle_deg:<10} {p.radius:<7}")

    draw_particles(particles, B_FIELD)
