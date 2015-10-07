/*
<BookmarkletInfo>
	<Name>
		Copy Record Id
	</Name>
	<Description>
		<strong>Copies the record id to the clipboard.</strong>  If copy to clipboard is not supported then provides you with a prompt window to copy the link.
	</Description>
</BookmarkletInfo>
*/
// based from https://github.com/gotdibbs/Dynamics-CRM-Bookmarklets
try {
    var id = formContext.Xrm.Page.data.entity.getId();
    if (!id) {
        return alert('Failed to find id on this form.');
    }
    
    if (window.clipboardData) {
        window.clipboardData.setData('Text', id);
    }
    else { 
        window.prompt('Copy to clipboard: Ctrl+C, Enter', id);
    }
}
catch(er) {
    alert('Error occurred while retrieving record id. '+ er.message);
}