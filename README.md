# Virtual Reality Visualiztion of Ocular Hemodynamic Models
***
This project was part of the Training and Experimentation in Computational Biology (TECBio) program, a Research Experiences for Undergraduates program funded by the National Science Foundation at the University of Pittsburgh. I collaborated with Dr. Ian Sigal and his Laboratory of Ocular Biomechanics to develop a virtual reality application to better visualize the Lamina Cribrosa blood vessel network in regards to Glaucoma. By leveraging a mathematical-coordinate based representation, the vasculature was able to be visualized in great detail while highlighting key features such as morphology and blood flow rate. The TECBio program concluded in a symposium where I presented the final [poster](https://drive.google.com/file/d/129E8023Ujuc2VJ1I9BCYn-55cDtdfcPB/view).

*Program Written in May 2021 - July 2021*  
*Pushed to Github in August 2021*

***

## Implementation Overview

Unity Game Engine was used to develop the complete virtual reality framework. The VR environment for the LCGraph scene consisted of the **Plane**, **VR Rig**, and the necessary **Prefabs**. Unity VR provides a base API with many features that allow for VR compatability and so the VR Rig implementation was a simple extension of the base API. For example, the left and right hands, the total camera offset, tracked pose driver, XR direct interaction, and XR controller functions were mapped to the VR Rig. The Prefabs that were used included Hand Models, Nodes, and Edges. The Hand Models were imported, Spheres were used for Nodes, and Line Renderers were used for the Edges. The implementation also included several C# scripts for which the roles have been defined below.

**[Click here for more details regarding Unity VR](https://docs.unity3d.com/540/Documentation/Manual/VROverview.html)**

***

## Source Files and Their Roles
