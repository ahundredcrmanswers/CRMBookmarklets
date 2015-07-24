/*
<BookmarkletInfo>
	<Name>
		Show OptionSet Values
	</Name>
	<Description>
		Opens up a window and displays the option set and their possible values
	</Description>
</BookmarkletInfo>
*/
formContext.Xrm.Page.ui.controls.forEach(function(c, i){
	if(c.getControlType()=="optionset"){
		var osv="<br /><b><u>Name: "+c.getName()+"</u></b><br />";
		formContext.$("#"+c.getName()+"_i").find("option").first().nextAll().each(function(){
			osv+="<div><i>Value:</i> "+$(this).attr("value")+" - <i>Text:</i> "+$(this).attr("title")+"</div>";
		});osa+="<div>"+osv+"</div>";
	}
});(window.open("#",
"#").document.open()).write("<div style='font-family:Segoe UI,Arial;font-size:11px;overflow:always'>"+osa+"</div>")