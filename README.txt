This is code written by Michael Townsend
If you have any questions please contact me at michaeltownsend99@gmail.com 

When reading through the code, start with MainPage as it contains the most detailed comments and 
important methods which are all referenced in the comments of other classes

This program is a notes taking/knowledge curation application, with two pages for journal style and free-floating whiteboard
style working.
Users can use different boxes for taking different types of boxes. 


Notes box:
This is for basic text note taking, and is saved as text in the save file.
NOTE that currently the save file uses basic punctuation to seperate data in the save file, so using this in the contents of this
box could cause issues. This is easy to change if neccesary.


Code box:
This is similar to the notes box, however python code can be written into this, and when the play button is pressed,
the code is run in cmd and the shell is displayed on the screen. This allows for diagram drawing to also be done as
the shell will create additional windows to show any plots.

NOTE that this may not work as it relies on python being installed


Diagram box:
This is for doing any windows ink drawing or handwriting in using a smart pen. It has handwriting recognition, and also can 
recognise basic shapes which is useful for flowchart and diagram drawing. This will be particulary useful for devices like 
the surface when used in the floating whiteboard with a smart pen.

Math box:
This was intended to be able to recognise mathmatical notation using an SDK from a company called iink. Unfortunately, although
this worked in a standalone application I was unable to get it to work inside this application. This was due to it being unable
to save and load for an unknown reason. After much discussion with teh developers of the SDK they discovered it was an issue
with their software and would be fixed "in the future". With this is mind I have stopped devlopment for this feature currently,
and any use of the math boxes in the app will cause the applciation to crash. This can be avoided by disabling the buttons for 
adding the math box in the app until functionality is added.

Dialog box:
This is the basis of the journal stlye page but can be used in the whiteboard for additional structure. It is a container in
which you can create a stack of of the other types of boxes. It has an infinite size and is scrollable.


In journal style page of the application, many of the boxes have a green and red button. The red button is to delete the box from the 
page and is not reversible. The green button is for the connection echanism between the boxes, which is very useful in large pages
for note taking. This mechanism is described below.


Connections:
Pressing the green button on a box will display an overlay. If there are no connections for the box, the user will be prompted to make
a connection or close the box. If there are connections, they will displayed in a scrollable area, a box can have inifinitely many 
connections.

If there are already connections, there will be buttons to add or remove a connection. Removing a connection simply removes it from that
boxes connections list. Adding a connection will take the user back to the note taking page they were on and the next step is for the user 
to click on the box they want to connect to. The user is able to scroll horizontally but not vertically within each individual dialog,
this functionality could be added in the future.

Once this box is clicked on, the overlay returns with the new connection added.


Future development:
Due to the time constraints and changing requirements of the project, the code is quite messy and refactoring it
to be more concise would be desirable. For example, all of the boxes share methods and so it would be useful to have a parent
class of generic box, which the other boxes inherit from.
Also if a box is connected to more than one other box, they all contain a copy of that original box which is inefficient.
In addition, the creation of a new box requires many lines of code each time, which could be moved into the constructor of the box
to make the code in the pages neater. Other inporvements have ben noted in the comments for that particular section.







