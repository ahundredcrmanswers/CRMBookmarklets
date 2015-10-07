/*
<BookmarkletInfo>
	<Name>
		Show All fields
	</Name>
	<Description>
		Sets the visibility to true for all fields on the form.
	</Description>
</BookmarkletInfo>
*/

// based from https://github.com/gotdibbs/Dynamics-CRM-Bookmarklets
formContext.Xrm.Page.ui.controls.forEach(function(c, i){
    c.setVisible(true);
});

formContext.Xrm.Page.ui.tabs.forEach(function(c, i){
    c.setVisible(true);
    
    c.sections.forEach(function(sc, si){
        sc.setVisible(true);
    });
});