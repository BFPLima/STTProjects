Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(onEachRequest);


function onEachRequest(sender, args) {
    initializeRequestHandler(sender, args);
}

// Sys.WebForms.PageRequestManager.getInstance().add_initializeRequest(initializeRequestHandler);

function configureFormValidation(newRules, newMessages) {

    validation.settings.rules = newRules;
    validation.settings.messages = newMessages;
    validation = $('#form1').validate();

}

function initializeRequestHandler(sender, args) {

    if (args.get_postBackElement().id == '<%=btnAddNewProject.ClientID%>') {

        configureFormValidation(validationRulesAddNewProject, validationMessagesAddNewProject);

    }
    else if (args.get_postBackElement().id == '<%=btnUpdateProject.ClientID%>') {

        configureFormValidation(validationRulesEditProject, validationMessagesEditProject);

    }

    if ($("#form1").valid() !== true) {
        args.set_cancel(true);
    }

}