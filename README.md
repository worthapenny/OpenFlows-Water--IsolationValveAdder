# Add Isolation Valves to a hydraulic model

The tool allows to add isolation valves to the hydraulic model based on given criteria. It supports ([WaterGEMS](https://www.bentley.com/en/products/product-line/hydraulics-and-hydrology-software/watergems)/[WaterCAD](https://www.bentley.com/en/products/product-line/hydraulics-and-hydrology-software/watercad)/WaterOPW).

## Download

Make sure to download the right version of the application. The OpenFlows-Water (OFW) is relative new API, so newer version of Water products are currently supported

### [Download v10.4.x.x](OFW.IsolationValveAdder/_setup.bat)

## Setup (Must Do!)

After extracting the contents from the compressed file, paste them over to the installation directory (typically: `C:\Program Files (x86)\Bentley\WaterGEMS\x64`).

## How to run

Open up the `OFW.IsolationValveAdder.exe` and screen like below loads.

Once the windows loads, open up the model and adjust the criteria on the left. Click on the green arrow (Compute) button for generating the Isolation Valves.

>From the toolbar, Element Symbology window can be opened to change the visibility of the Isolation Valve,in case the layer is turned off.

![isolation_valve_adder_form](https://github.com/worthapenny/OpenFlows-Water--ModelMerger/blob/images/isolation_valve_adder_form.png "Isolation Valve Adder Form")

## Other projects based on OpenFlows Water and/or WaterObjects.NET

* [Bing Background Adder]([http://#](https://github.com/worthapenny/OpenFlows-Water--BingBackground))
* [Model Merger]([http://#](https://github.com/worthapenny/OpenFlows-Water--ModelMerger))
* [Isolation Valve Adder]([http://#](https://github.com/worthapenny/OpenFlows-Water--IsolationValveAdder))

## Did you know?

Now, you can work with Bentley Water products with python as well. Check out:

* [Github pyofw](https://github.com/worthapenny/pyofw)
* [PyPI pyofw](https://pypi.org/project/pyofw/)

![pypi-image](https://github.com/worthapenny/OpenFlows-Water--ModelMerger/blob/main/images/pypi_pyofw.png)