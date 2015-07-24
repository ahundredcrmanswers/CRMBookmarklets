/*
<BookmarkletInfo>
	<Name>
		Show Schema Names
	</Name>
	<Description>
		Changes all the labels on the form to display their schema names instead of their display names.
	</Description>
</BookmarkletInfo>
*/
// based from https://github.com/gotdibbs/Dynamics-CRM-Bookmarklets
formContext.Xrm.Page.ui.controls.forEach(function(c, i){
    c.setLabel(c.getName());
});