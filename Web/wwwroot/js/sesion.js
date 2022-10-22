(function () {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/sesionHub")
        .build();

    var namespace;
    namespace = {
        solicitarControlEvento: function (editorId, eventoId, currentUserName, currentUserId, encabezadoEvento) {
            connection.invoke("SolicitarControlEvento", editorId, eventoId, currentUserName, currentUserId, encabezadoEvento).catch(err => console.error(err));
        },
        concederControlEvento: function (eventoId, solicitanteUserId, encabezadoEvento) {
            $('#noti_' + eventoId).find('[data-notify="dismiss"]').trigger("click");
            connection.invoke("AceptarControlEvento", eventoId, solicitanteUserId, encabezadoEvento).catch(err => console.error(err));
            if ($('#navEventoid').val() && $('#navEventoid').val() == eventoId) {
                console.log('AAAAAAAAAAAA');
                window.location.href = window.location.href;
            }
        },
        rechazarControlEvento: function (eventoId, solicitanteUserId, encabezadoEvento) {
            $('#noti_' + eventoId).find('[data-notify="dismiss"]').trigger("click");
            connection.invoke("RechazarControlEvento", eventoId, solicitanteUserId, encabezadoEvento).catch(err => console.error(err));
        }
    };

    window.ns = namespace;

    connection.on("RecibirNotificacionRechazo", (message) => {
        $('#linkNotificaciones').addClass('top-nav__notify');
        const el = document.createElement("div");
        el.className = "listview__item";
        el.innerHTML = `<div class='listview__content'><div class='listview__heading'>Rechazada</div><p>${message}</p></div>`;
        document.getElementById("notificationList").appendChild(el);

        $.notify({
            message: `${message}`
        }, {
                type: "danger",
                placement: {
                    from: "bottom",
                    align: "center"
                },
                delay: 3000
            });

    });

    connection.on("RecibirNotificacionAcepta", (message, eventoId, solicitanteUserId) => {
        $('#linkNotificaciones').addClass('top-nav__notify');
        const el = document.createElement("div");
        el.className = "listview__item";
        el.innerHTML = `<div class='listview__content'><div class='listview__heading'>Aceptada</div><p>${message}</p></div>`;
        document.getElementById("notificationList").appendChild(el);

        $.get(`../../api/Eventos/${eventoId}/CambiarEditorRetornandoUrl/${solicitanteUserId}`).done(function (data) {
            $.notify({
                message: `${message}`
            }, {
                    type: "success",
                    placement: {
                        from: "bottom",
                        align: "center"
                    },
                    delay: 4000,
                    onClosed: function () {
                        window.location.href = data;
                    }
                });
        });
    });


    connection.on("ControlEventoSolicitado", (message, eventoId, currentUserId, currentUserName, encabezadoEvento) => {

        const encodedMsg = message + ' , ' + eventoId + ' , ' + currentUserId;
        $('#linkNotificaciones').addClass('top-nav__notify');

        const el = document.createElement("div");
        el.className = "listview__item";
        el.innerHTML = `<div class='listview__content'>
                            <div class='listview__heading'>${currentUserName}</div>
                            <p>${message}</p>
                        </div>`;
        document.getElementById("notificationList").appendChild(el);

        $.notify({
            message: `${currentUserName} ${message}`
        }, {
                allow_dismiss: true,
                type: "dark",
                placement: {
                    from: "top",
                    align: "center"
                },
                delay: 0,
                template: `<div data-notify='container' class='col-xs-11 col-sm-3 alert alert-{0}' role='alert' id='noti_${eventoId}'> 
                    <span data-notify='title'>{1}</span>  
                    <span data-notify='message'>{2}</span>
                    <div class="text-right">
                         <button class="btn btn-success btn--icon"  onclick='ns.concederControlEvento("${eventoId}","${currentUserId}","${encabezadoEvento}")'><i  class="zmdi zmdi-check"></i></button>
                         <button class="btn btn-danger btn--icon" onclick='ns.rechazarControlEvento("${eventoId}","${currentUserId}","${encabezadoEvento}");'><i  class="zmdi zmdi-close"></i></button>
                    </div>  
                    <div class='progress' data-notify='progressbar'>
                    <div class='progress-bar progress-bar-{0}' role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' style='width: 0%;'></div>
                    </div>
                    <span style='display:none'>{3} {4}</span> 
                    <button type="button" aria-hidden="true" data-notify="dismiss" class="alert--notify__close"></button>
                    </div>`
            });

    });

    connection.start().catch(err => console.error(err));

})();
