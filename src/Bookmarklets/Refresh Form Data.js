/*
<BookmarkletInfo>
	<Name>
		Refresh From data
	</Name>
	<Description>
		Asynchronously refresh the form.
	</Description>
</BookmarkletInfo>
*/
try {
    formContext.Xrm.Page.data.refresh(false);
}
catch(er) {
    alert('Error occurred while retrieving record id. '+ er.message);
}