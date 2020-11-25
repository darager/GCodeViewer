# 5D-Slicer

![Tests](https://github.com/darager/GCodeViewer/workflows/.NET%20Core/badge.svg)

This tool enables the creation of .gcode files for a custom built 5D-Printer.

**Workflow to create a gcode file for the 5D-printer:**

1. configure the printbed and printer dimensions
1. import and .stl model
1. position the model on the printbed in the 3D-view
1. cut the model where the A and C axes should be used
1. configure the printing settings
1. slice the configured model pieces using the cura engine
1. look over the generated gcode file for errors and similar in the live editor
1. export the gcode file and print using Repetier Host


## Cut Meshes using Gradientspace/geometry3Sharp

- STLReader/Writer
- MeshPlaneCut
- RemoveDuplicateTriangles


## Use gsSlicer to generate g-code

- GenerateGCodeForMeshes()

Look at https://github.com/gradientspace/gsSlicerApps/sliceViewGTK/SliceViewerMain.cs for an example.
