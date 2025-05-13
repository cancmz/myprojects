import os
import subprocess

base_dir = os.path.dirname(os.path.abspath(__file__))
results_dir = os.path.join(base_dir, "results")
if not os.path.exists(results_dir):
    os.makedirs(results_dir)

detector_path = os.path.join(base_dir, "detector_sim.py")
analysis_path = os.path.join(base_dir, "analysis.py")

print("[1/2] Running detector_sim.py...")
subprocess.run(["python", detector_path], check=True)

print("[2/2] Running analysis.py...")
subprocess.run(["python", analysis_path], check=True)

print("\n All steps completed. Check the 'results' folder for outputs.")

