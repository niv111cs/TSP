

# Simulated Annealing for TSP with Live Visualization

This project implements a graphical simulation of the **Traveling Salesman Problem (TSP)** using the **Simulated Annealing** algorithm in C# with Windows Forms.

## Project Overview

- The algorithm searches for the shortest possible route that visits a set of points exactly once and returns to the starting point.
- The optimization process is visualized in real time using a Windows Forms interface.
- During execution, the current path and its cost are displayed dynamically.
- The best path found throughout the run is stored and shown at the end.

## Key Features

- Simulated Annealing logic implemented from scratch.
- Real-time graphical visualization of the current route.
- Live display of the current total cost (route length).
- Auto-scaling and centering of points on the screen.
- Easy to modify the initial set of coordinates.

## How It Works

- At each iteration, two cities (excluding the starting point) are randomly swapped.
- The new solution is accepted based on the cost difference and current temperature.
- The temperature gradually decreases over time.
- The algorithm stores and eventually returns the best solution found during the entire process.

## Technologies Used

- Language: **C#**
- Platform: **.NET Framework (Windows Forms)**
- UI: **GDI+ drawing for visualization**

## Usage

- Open the project in Visual Studio.
- Run the application.
- A window will open showing the path evolving over time.
- At the end, the best path and its total cost will be displayed.

## Customization

To use your own set of points, modify the `curr_points` array in `Program.cs`.

```csharp
PointF[] curr_points = new PointF[]
{
    new PointF(4, 0),
    new PointF(0, 2),
    ...
};
