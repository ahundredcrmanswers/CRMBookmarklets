/*
<BookmarkletInfo>
	<Name>
		Open Performance Report
	</Name>
	<Description>
		Opens the PerformanceUI window, if supported by your version of CRM.
	</Description>
</BookmarkletInfo>
*/
// based from https://github.com/gotdibbs/Dynamics-CRM-Bookmarklets

try {
    if (formContext.APPLICATION_VERSION == '6.0') {
        if (typeof formContext._IsRefreshForm != 'undefined' && formContext._IsRefreshForm == '1') { 
            formContext.OpenPerformanceUI(true);
        } 
        else { 
            formContext.OpenPerformanceUI(); 
        }
    }
    else if (formContext.APPLICATION_VERSION === '6.1' ||
        /^7\.\d+$/.test(formContext.APPLICATION_VERSION)) {
        formContext.Mscrm.Performance.PerformanceCenter.get_instance().TogglePerformanceResultsVisibility();
    }
    else {
        alert('This feature is not supported on this version of Dynamics CRM.');
    }
}
catch(er) {
    alert('Error occurred opening performance report. '+ er.message);
}