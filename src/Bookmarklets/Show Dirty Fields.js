/*
<BookmarkletInfo>
	<Name>
		Show Dirty Fields
	</Name>
	<Description>
		Alerts a message listing all the dirty attributes (fields that have changed) on the form.
	</Description>
</BookmarkletInfo>
*/
// based from https://github.com/gotdibbs/Dynamics-CRM-Bookmarklets
var dirtyAttributes = [];

formContext.Xrm.Page.data.entity.attributes.forEach(function(c, i){
    if (c && c.getIsDirty && c.getIsDirty()) {
        dirtyAttributes.push(c.getName());
    }
});

if (!dirtyAttributes || !dirtyAttributes.length) {
    alert('No attributes appear to be dirty on the current form.');
}
else {
    alert(['The following attributes are currently dirty: \n', 
        dirtyAttributes.join(', ')].join(''));
}