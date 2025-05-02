# CERN-Inspired Particle Detector Simulation

This project simulates a basic particle detection system inspired by real-world experiments in high-energy physics, such as those conducted at CERN. It models how incoming particles are detected over time, incorporating statistical behavior and time-dependent flux.

## Overview

The simulation estimates how many particles arrive at a detector each second and how many of those are detected based on predefined probability. The flux of incoming particles varies over time using a sinusoidal function, representing fluctuations observed in cosmic radiation due to solar and geomagnetic activity.

## Scientific Context

This simulation reflects key ideas in particle physics experiments:

- Cosmic particle flux is variable and probabilistic.
- Detectors only capture a fraction of incoming particles.
- Time-resolved data is crucial for analysis.

Environmental and cosmic conditions such as solar flares, geomagnetic storms and altitude influence particle flux and detection:

| Real-World Condition                 | Simulation Parameter Affected       | Example                                     |
|-------------------------------------|-------------------------------------|---------------------------------------------|
| Increased solar activity (e.g., CME)| `BASE_FLUX`, `FLUX_AMPLITUDE`       | Sudden rise in incoming particles           |
| Earth's magnetic field fluctuations | `FLUX_PERIOD` or wave shape         | Longer or irregular flux variation          |
| High-altitude measurement (e.g., balloon or ISS) | `BASE_FLUX`                   | Higher constant particle rate               |
| Detector degradation or shielding   | `HIT_PROBABILITY`                   | Lower detection probability despite flux    |

Although simplified, this model mimics the core behavior of real-world detectors and can serve as a learning tool or a foundational project for more advanced simulations.

## Features

- Time-based simulation (default duration: 60 seconds)
- Sinusoidal variation in incoming particle flux
- Probabilistic detection model (default: 10% detection probability)
- Optional noise and randomness in flux
- CSV data output and visual graph generation

## Files

- `main.py`: Executes the entire simulation and plotting process
- `detector_sim.py`: Simulates incoming particles and writes results to CSV
- `analysis.py`: Reads CSV and generates a plot comparing hits vs. incoming particles
- `results/`: Folder where outputs are saved:
  - `counts.csv`: Simulation data
  - `plot.png`: Resulting graph

## Simulation Parameters

| Parameter         | Description                                  | Default |
|------------------|----------------------------------------------|---------|
| BASE_FLUX        | Average number of incoming particles/second  | 10      |
| FLUX_AMPLITUDE   | How much the particle flux varies            | 5       |
| FLUX_PERIOD      | How often the flux wave repeats (in seconds) | 20      |
| HIT_PROBABILITY  | Probability of detecting a single particle   | 0.10    |
| DURATION         | Simulation duration in seconds               | 60      |

These can be changed in `detector_sim.py` to simulate different environments.

## How to Run

Install the required libraries if not already installed:

pip install pandas matplotlib


Then run the simulation:


This will generate:
- `results/counts.csv`: Particle data over time
- `results/plot.png`: Line graph showing detected vs. incoming particles


## Author

Ahmet Can Çömez  
Computer Engineering Student  
