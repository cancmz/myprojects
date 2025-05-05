# CERN-Inspired Charged Particle Tracker

This project simulates the motion of charged particles in a uniform magnetic field, inspired by particle tracking detectors used in CERN experiments such as ATLAS and CMS. The goal is to model the curved trajectories of charged particles under the influence of the Lorentz force, and to visualize their paths based on physical parameters like momentum, charge, and angle.

## Overview

Charged particles launched from the origin with varying momentum and angle move in circular paths when exposed to a magnetic field. Their trajectories are computed using the classical relationship between momentum, magnetic field, and curvature radius. The simulation plots the path of each particle, visualizes its curvature direction depending on charge, and marks its starting point and direction.

## Features

- Fully SI-unit-based calculation and visualization
- Adjustable particle count, momentum range, angle range, and magnetic field
- Sign-based curvature: clockwise for negative, counterclockwise for positive charge
- Vector arrows showing direction of motion
- Initial position markers at (0, 0)
- Optional reference axes and annotations
- High-resolution plot export with timestamped filenames

## Files

- `tracker_sim.py`: Main simulation script; generates and plots particle paths
- `results/`: Folder for output plots:
  - `plot_<timestamp>.png`: High-quality image of simulated tracks

## Simulation Parameters

| Parameter         | Description                                       | Default            |
|------------------|---------------------------------------------------|--------------------|
| NUM_PARTICLES    | Number of particles to simulate                   | 10                 |
| B_FIELD          | Magnetic field strength (Tesla)                   | 1.0                |
| MOMENTUM_RANGE   | Momentum range (kg·m/s) for particles             | (1, 3)             |
| ANGLE_RANGE      | Launch angle range (degrees)                      | (0°, 180°)         |
| ARC_FRACTION     | Fraction of a full circle drawn for each track    | 0.5 (i.e. 180°)    |

You can customize these constants directly in the `tracker_sim.py` file.

## How to Run

Make sure you have the required library:

```bash
pip install matplotlib
```

Then execute the simulation:

```bash
python tracker_sim.py
```

This will generate a high-resolution plot in the `results/` folder with a timestamped filename.


## Scientific Context

This simulation is based on the classical Lorentz force law:

F = q · (v × B)  ⟶  r = p / (|q| · B)

Where:
- `r` = radius of curvature (meters)
- `p` = momentum (kg·m/s)
- `q` = charge (Coulombs)
- `B` = magnetic field strength (Tesla)


This system captures how a uniform magnetic field bends the path of charged particles. The direction of curvature depends on the sign of the charge, and the radius is inversely proportional to the magnetic field and the charge magnitude.

## Example Output

- Particle paths are color-coded by charge:
  - Blue: positive
  - Red: negative
- Arrows show the direction of motion
- Starting point of each particle is marked
- Coordinate axes (x=0, y=0) are indicated for reference
- Exported with high DPI (600) for publication-quality graphics

## Author

**Ahmet Can Çömez**  
Computer Engineering Student  
Interested in physics, simulation, and scientific modeling.
