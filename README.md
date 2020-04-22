# 5D-Slicer

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
