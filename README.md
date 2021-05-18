# stock_deprecator

Requires 
==========
.NET Core 3.1
Visual Studio 2019

How To Use
==========
Simply load up the solution in Visual Studio and run the "StockDeprecatorMVC" project.

You should be presented with a rather simple landing page. Click the "Current Stock" option in the main menu bar. This will drop you into a page listing the stock with sell in and quality values from the spec. document.

Clicking the "Deprecate" btton will run the deprecation process and present the results. 

Clicking "Back" will return the user to the stock list where they can update the stock details manually or just run the deprecate process again. (In a live system I would probably have put some safeguards in to prevent stock being deprecated multiple times on the same day).

Data
========
Both the stock and rules configuration come from files (as a surrogate for a database in a live system) and can be found in the "Data" folder in the solution. 
* Stock data is stored in a flat ".txt" file named "Stock.txt"
* Rule configuration data is stored in a ".json" file named "StockTypes.json"

Changes to both data stores are saved to memory so if the content of these files is changed you will need to restart the application to see the changes.
