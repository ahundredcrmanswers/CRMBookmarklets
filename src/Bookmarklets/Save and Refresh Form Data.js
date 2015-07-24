/*
<BookmarkletInfo>
	<Name>
		Shave and Refresh From data
	</Name>
	<Description>
		Asynchronously save and refresh the form.
	</Description>
</BookmarkletInfo>
*/
try {
    formContext.Xrm.Page.data.refresh(true);
}
catch(er) {
    alert('Error occurred while retrieving record id. '+ er.message);
}