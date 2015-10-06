# CRMBookmarklets
Project for building and maintaining simple Bookmarklets to be used within CRM


Bookmarklets are unobtrusive JavaScripts stored as the URL of a bookmark in a web browser or as a hyperlink on a web page. Bookmarklets are usually JavaScript programs. Regardless of whether bookmarklet utilities are stored as bookmarks or hyperlinks, they add one-click functions to a browser or web page. When clicked, a bookmarklet performs one of a wide variety of operations, such as running a search query or extracting data from a table. For example, clicking on a bookmarklet after selecting text on a webpage could run an Internet search on the selected text and display a search engine results page.?

See Wikipedia more more detail: https://en.wikipedia.org/wiki/Bookmarklet?

With CRM, there are some cases where you would like to change the form input fields, retrieve data from the form (ie Guid) to use in other processes.

This project allows for the simple maintenance of a list of javascript bookmarklets that can be useful when using CRM.  The intended goal of these bookmarklets are for advanced users/developers who know what they are doing, and not for production based users as the changes made to the page may not be supported by Microsoft.

This project is a simple way to write CRM bookmarklets in javascript and build them in a list of links that can then be easily added to your bookmarks to be used as bookmarklets.

# Bookmarklet Builder
This project consists of a C# program that will read the javascript bookmarklet files and wrap them into a single html output file.  This file can then be opened and you can create your bookmak in your browser from the links.  Click here to view the Pre-Built [CRM Bookmarklets](src/Bookmarklets.html)

# Bookmarklets
In the [Bookmarklets folder](src/Bookmarklets/) contains all the bookmarklets javascript files.

Some example bookmarklets:
* Copy the Record Id - copies the record id to the clipboard if available or opens an input dialog so the user can copy it.
* Enable All Fields - Enables all visible fields on the form to all you to edit them.
* show Schema Names - Changes all input labels form their display name to the their schama name.

Bookmarklets.js.template - this is a template file that can be copied from to make a new bookmarklets file.  Just copy and rename it with the ".js" extension in the Bookmaklets folder.  From there you can edit you new bookmarklet file.  

The following is the template that you can edit.
```javascript
/*
<BookmarkletInfo>
	<Name>
		<!-- Specify the bookmarklet name here to show up in the Bookmarklets.html file -->
	</Name>
	<Description>
		<!-- Specify the description of the bookmarklet that is below the bookmarklet name in the Bookmarklets.html file.  This description can contain HTML formatting. -->
	</Description>
</BookmarkletInfo>
*/
// formContext - this is the variable passed into this javascript from BookmarkletMain.js wrapper
// formContext.Xrm.Page - this is the Xrm Page object
try {
	
	// Write your bookmarklet logic here, using the fromContext to extarct and/or manipulte
    var id = formContext.Xrm.Page.data.entity.getId();
    if (!id) {
        return alert('Failed to find id on this form.');
    }
	return alert('Record Guid: ' + id);
}
catch(er) {
	// handle exception
    alert('Error: '+ er.message);
}
```

Once you have created your new bookmarklet to add to the list, you can then re-generate the [Bookmarklets.html](src/Bookmarklets.html) buy running the Bookmarklet Builder src/BookmarkletBuilder.exe.  Note: you can modify the configution for the Bookmarklet Builder executable by editing the config file: [src/BookmarkletBuilder.exe.config](src/BookmarkletBuilder.exe.config)


Credit