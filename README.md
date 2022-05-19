# Virtual Reality Visualiztion of Ocular Hemodynamic Models
This project was part of the Training and Experimentation in Computational Biology (TECBio) program, a Research Experiences for Undergraduates program funded by the National Science Foundation at the University of Pittsburgh. I collaborated with Dr. Ian Sigal and his Laboratory of Ocular Biomechanics to develop a virtual reality application to better visualize the Lamina Cribrosa blood vessel network in regards to Glaucoma. By leveraging a mathematical-coordinate based representation, the vasculature was able to be visualized in great detail while highlighting key features such as morphology and blood flow rate. The TECBio program concluded in a symposium where I presented the final [poster](https://drive.google.com/file/d/129E8023Ujuc2VJ1I9BCYn-55cDtdfcPB/view).

*Program Written in May 2021 - July 2021*  
*Pushed to Github in August 2021*


## Implementation Overview

Unity Game Engine was used to develop the complete virtual reality framework. The VR environment for the LCGraph scene consisted of the **Plane**, **VR Rig**, and the necessary **Prefabs**. Unity VR provides a base API with many features that allow for VR compatability and so the VR Rig implementation was a simple extension of the base API. For example, the left and right hands, the total camera offset, tracked pose driver, XR direct interaction, and XR controller functions were mapped to the VR Rig. The Prefabs that were used included Hand Models, Nodes, and Edges. The Hand Models were imported, Spheres were used for Nodes, and Line Renderers were used for the Edges. The implementation also included several C# scripts for which the roles have been defined below.

**[Click here for more details regarding Unity VR](https://docs.unity3d.com/540/Documentation/Manual/VROverview.html)**


## Source Files and Their Roles

All source files are stored in the `Assets/Scripts/` folder. The following files are of key importance:

---

`CSVReader.cs` is the main program that parses the different databases. While parsing the data, it converts values to ints/floats if possible and returns a `List<Dictionary<string, object>>`. 

---

`GraphPlotter.cs` uses `CSVReader.cs` to plot the respective nodes and edges. This is done by first initializing the necessary GameObjects which are the Prefabs and an object to contain the instantiated Prefabs. For the nodes, this includes `NodePrefab` and `NodeHolder` and for the edges, this includes `lineComponent` and `EdgePrefab`. In the `Start()` function, which is called before the first frame update, the node and edge data is read in as lists.<br>
For the Nodes:
1. After extracting the x, y, and z coordinates for each node from the `nodeDataList`, a new GameObject is instantiated using the Prefab and a `Vector3` object to represent the coordinates.
2. Each node is made a child object of the `NodeHolder` object.

For the Edges:
1. After extracting the x-coordinates, y-coordinates, z-coordinates (starting and ending positions), flow rate, and the RGB values for each edge from the `edgeDataList`, a new GameObject is instantiated with the object type as `LineRenderer`. 
2. Each `lineComponent` is given a starting and ending position correlating with the six coordinates as `Vector3` objects. The `lineComponent` is also given a standardized weight. 
3. If the user requests a flow rate representation, then each edge is set to the color of its RGB value representing its flow rate where cooler colors represent lower flow rates and warmer colors represent higher flow rates. Otherwise, each edge is set to the default color of base red. 
4. Each edge is made a child object of the `EdgePrefab` object.

This file also includes two functions `FindMaxValue()` and `FindMinValue()` that are used to normalize the graph in order to make it easier to view in the VR scene.

---

`ContinuousMovement.cs` is the script that controls the overall movement and fluidity within the application. This is done by simultaneously updating the right and left controllers through the `Update()` function which is called once per frame. The right controller enables movement in the x and z directions while the left controller enables movement in the y direction. The `speed` variable controls the rate of movement. The `FixedUpdate()` function handles head yaw which uses the headset's camera's rotation to control the direction of movement on the x and z axes.

---

`HandPresence.cs` is the script that controls the hand models. This is done by first ensuring that the controller devices are connected, instantiating prefabs for each controller, and then transforming them to resemble hands. These prefabs are given an `Animator` component that allows certain pressed keys to trigger animations. The `UpdateHandAnimation()` function receives the input from the controllers and then updates the animation accordingly. The "hands" can perform the following actions:
* Pinch: Press the trigger button on the controller
* Grip: Press the inside grip button on the controller

These actions were mapped to the individual keys on the controller by using Unity's Blend Tree feature. Each animation is set by the intensity of the key press where the intensity is a float. If the key is not pressed at all then the intensity is set to 0. This is continuously monitored by the `Update()` function which calls `UpdateHandAnimation()`.

## Executing and Understanding the Output

**EXECUTION:**

***

## Footnotes

