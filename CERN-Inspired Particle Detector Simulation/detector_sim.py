def run_simulation():
    import random as rnd
    import csv
    import math
    import os

    BASE_FLUX = 10
    FLUX_AMPLITUDE = 5
    FLUX_PERIOD = 20
    DURATION = 60
    HIT_PROBABILITY = 0.1

    os.makedirs("results", exist_ok=True)

    with open("results/counts.csv", "w", newline="") as file:
        writer = csv.writer(file)
        writer.writerow(["Second", "Incoming", "Hits"])

        for time in range(DURATION):
            flux = BASE_FLUX + FLUX_AMPLITUDE * math.sin(2 * math.pi * time / FLUX_PERIOD)
            flux += rnd.uniform(-1, 1)
            particles_per_second = max(0, int(round(flux)))

            hits = 0
            for _ in range(particles_per_second):
                if rnd.random() < HIT_PROBABILITY:
                    hits += 1
            print(f"Time: {time + 1}s → Incoming: {particles_per_second}, Hits: {hits}")
            writer.writerow([time + 1, particles_per_second, hits])

if __name__ == "__main__":
    run_simulation()
