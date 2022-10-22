$(document).ready(function ($) {

    "use strict";

    var loader = function () {

        setTimeout(function () {
            if ($('#pb_loader').length > 0) {
                $('#pb_loader').removeClass('show');
            }
        }, 700);
    };
    loader();

    // scroll
    var scrollWindow = function () {
        $(window).scroll(function () {
            var $w = $(this),
                st = $w.scrollTop(),
                navbar = $('.pb_navbar'),
                sd = $('.js-scroll-wrap');

            if (st > 150) {
                if (!navbar.hasClass('scrolled')) {
                    navbar.addClass('scrolled');
                }
            }
            if (st < 150) {
                if (navbar.hasClass('scrolled')) {
                    navbar.removeClass('scrolled sleep');
                }
            }
            if (st > 350) {
                if (!navbar.hasClass('awake')) {
                    navbar.addClass('awake');
                }

                if (sd.length > 0) {
                    sd.addClass('sleep');
                }
            }
            if (st < 350) {
                if (navbar.hasClass('awake')) {
                    navbar.removeClass('awake');
                    navbar.addClass('sleep');
                }
                if (sd.length > 0) {
                    sd.removeClass('sleep');
                }
            }
        });
    };
    scrollWindow();

    // slick sliders
    var slickSliders = function () {
        $('.single-item').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            dots: true,
            infinite: true,
            autoplay: false,
            autoplaySpeed: 2000,
            nextArrow: '<span class="next"><i class="ion-ios-arrow-right"></i></span>',
            prevArrow: '<span class="prev"><i class="ion-ios-arrow-left"></i></span>',
            arrows: true,
            draggable: false,
            adaptiveHeight: true
        });

        $('.single-item-no-arrow').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            dots: true,
            infinite: true,
            autoplay: true,
            autoplaySpeed: 2000,
            nextArrow: '<span class="next"><i class="ion-ios-arrow-right"></i></span>',
            prevArrow: '<span class="prev"><i class="ion-ios-arrow-left"></i></span>',
            arrows: false,
            draggable: false
        });

        $('.multiple-items').slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            dots: true,
            infinite: true,

            autoplay: true,
            autoplaySpeed: 2000,

            arrows: true,
            nextArrow: '<span class="next"><i class="ion-ios-arrow-right"></i></span>',
            prevArrow: '<span class="prev"><i class="ion-ios-arrow-left"></i></span>',
            draggable: false,
            responsive: [
                {
                    breakpoint: 1125,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 1,
                        infinite: true,
                        dots: true
                    }
                },
                {
                    breakpoint: 900,
                    settings: {
                        slidesToShow: 2,
                        slidesToScroll: 2
                    }
                },
                {
                    breakpoint: 580,
                    settings: {
                        slidesToShow: 1,
                        slidesToScroll: 1
                    }
                }
            ]
        });

        $('.js-pb_slider_content').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            fade: true,
            asNavFor: '.js-pb_slider_nav',
            adaptiveHeight: false
        });
        $('.js-pb_slider_nav').slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            asNavFor: '.js-pb_slider_content',
            dots: false,
            centerMode: true,
            centerPadding: "0px",
            focusOnSelect: true,
            arrows: false
        });

        $('.js-pb_slider_content2').slick({
            slidesToShow: 1,
            slidesToScroll: 1,
            arrows: false,
            fade: true,
            asNavFor: '.js-pb_slider_nav2',
            adaptiveHeight: false
        });
        $('.js-pb_slider_nav2').slick({
            slidesToShow: 3,
            slidesToScroll: 1,
            asNavFor: '.js-pb_slider_content2',
            dots: false,
            centerMode: true,
            centerPadding: "0px",
            focusOnSelect: true,
            arrows: false
        });
    };
    slickSliders();

    // navigation
    var OnePageNav = function () {
        var navToggler = $('.navbar-toggler');
        $(".smoothscroll[href^='#'], #probootstrap-navbar ul li a[href^='#']").on('click', function (e) {
            e.preventDefault();
            var hash = this.hash;

            $('html, body').animate({

                scrollTop: $(hash).offset().top
            }, 700, 'easeInOutExpo', function () {
                window.location.hash = hash;
            });
        });
        $("#probootstrap-navbar ul li a[href^='#']").on('click', function (e) {
            if (navToggler.is(':visible')) {
                navToggler.click();
            }
        });

        $('body').on('activate.bs.scrollspy', function () {
            console.log('nice');
        })
    };
    OnePageNav();

    var offCanvasNav = function () {
        var toggleNav = $('.js-pb_nav-toggle'),
            offcanvasNav = $('.js-pb_offcanvas-nav_v1');
        if (toggleNav.length > 0) {
            toggleNav.click(function (e) {
                $(this).toggleClass('active');
                offcanvasNav.addClass('active');
                e.preventDefault();
            });
        }
        offcanvasNav.click(function (e) {
            if (offcanvasNav.hasClass('active')) {
                offcanvasNav.removeClass('active');
                toggleNav.removeClass('active');
            }
            e.preventDefault();
        })
    };
    offCanvasNav();

    var ytpPlayer = function () {
        if ($('.ytp_player').length > 0) {
            $('.ytp_player').mb_YTPlayer();
        }
    };
    ytpPlayer();


    $('#Vm_Patente').on('change', function (e) {
        if (e.target.value.length === 6)
            performBusqueda(e.target.value);
    });

    $('#Vm_TipoVehiculo').change(function () {
        var tipoVehiculoSeleccinado = $("#Vm_TipoVehiculo").val();
        console.log('tipvs', tipoVehiculoSeleccinado);
        $.get(`/Vehiculos/api/Vehiculos/TiposVehiculos/${tipoVehiculoSeleccinado}/Marcas`).done(function (modelos) {
            $('#Vm_MarcaId').empty();
            $('#Vm_ModeloId').empty();
            $('#Vm_VersionId').empty();
            $("#Vm_MarcaId").prepend("<option value='' selected='selected'>Marca</option>");
            $("#Vm_ModeloId").prepend("<option value='' selected='selected'>Modelo</option>");
            $("#Vm_VersionId").prepend("<option value='' selected='selected'>Version</option>");

            $(modelos).each(function () {
                $("#Vm_MarcaId").append($("<option></option>").val(this.valor).html(this.texto));
            });
        });
    });


    $('#Vm_MarcaId').change(function () {
        var marcaSeleccionadaId = $("#Vm_MarcaId").val();
        var tipoVehiculoSeleccinado = $("#Vm_TipoVehiculo").val();
        if (tipoVehiculoSeleccinado) {
            $.get(`/Vehiculos/api/Vehiculos/TiposVehiculos/${tipoVehiculoSeleccinado}/Marcas/${marcaSeleccionadaId}/Modelos`).done(function (modelos) {
                $('#Vm_ModeloId').empty();
                $('#Vm_VersionId').empty();
                $("#Vm_ModeloId").prepend("<option value='' selected='selected'>Modelo</option>");
                $("#Vm_VersionId").prepend("<option value='' selected='selected'>Version</option>");
                $(modelos).each(function () {
                    $("#Vm_ModeloId").append($("<option></option>").val(this.valor).html(this.texto));
                });
            });
        } else {
            $.get(`/vehiculos/api/vehiculos/modelos/${marcaSeleccionadaId}`).done(function (modelos) {
                $('#Vm_ModeloId').empty();
                $('#Vm_VersionId').empty();
                $("#Vm_ModeloId").prepend("<option value='' selected='selected'>Modelo</option>");
                $("#Vm_VersionId").prepend("<option value='' selected='selected'>Version</option>");
                $(modelos).each(function () {
                    $("#Vm_ModeloId").append($("<option></option>").val(this.id).html(this.nombre));
                });
            });
        }

    });

    $('#Vm_ModeloId').change(function () {
        var marcaSeleccionadaId = $("#Vm_MarcaId").val();
        var tipoVehiculoSeleccinado = $("#Vm_TipoVehiculo").val();
        var modeloIdSeleccionado = $("#Vm_ModeloId").val();

        if (tipoVehiculoSeleccinado) {
            $.get(`/Vehiculos/api/Vehiculos/TiposVehiculos/${tipoVehiculoSeleccinado}/Marcas/${marcaSeleccionadaId
                }/Modelos/${modeloIdSeleccionado}/versiones`).done(function (modelos) {
                    $('#Vm_VersionId').empty();
                    $("#Vm_VersionId").prepend("<option value='' selected='selected'>Version</option>");
                    $(modelos).each(function () {
                        $("#Vm_VersionId").append($("<option></option>").val(this.valor).html(this.texto));
                    });
                });
        } else {
            $.get(`/Vehiculos/api/Vehiculos/Marcas/${marcaSeleccionadaId}/Modelos/${modeloIdSeleccionado}/versiones`).done(function (modelos) {
                $('#Vm_VersionId').empty();
                $("#Vm_VersionId").prepend("<option value='' selected='selected'>Version</option>");
                $(modelos).each(function () {
                    $("#Vm_VersionId").append($("<option></option>").val(this.valor).html(this.texto));
                });
            });
        }

    });



    $('[type="date"]').prop('min', function () {
        return new Date().toJSON().split('T')[0];
    });

    $('#Vm_Ciudad').change(function () {
        var ciudadSeleccionadaId = $("#Vm_Ciudad").val();
        $.get(`/dims/api/comunas/${ciudadSeleccionadaId}`).done(function (comunas) {
            $('#Vm_ComunaId').empty();
            $('#Vm_TallerId').empty();
            $("#Vm_ComunaId").prepend("<option value='' selected='selected'>Comuna</option>");
            $("#Vm_TallerId").prepend("<option value='' selected='selected'>Taller</option>");
            $(comunas).each(function () {
                $("#Vm_ComunaId").append($("<option></option>").val(this.id).html(this.nombre));
            });
        });
    });

    //$('#Vm_ComunaId').change(function () {
    //    var comundaSeleccionadaId = $("#Vm_ComunaId").val();
    //    $.get(`/talleres/api/Talleres/${comundaSeleccionadaId}`).done(function (talleres) {
    //        console.log('talleres', talleres, comundaSeleccionadaId);
    //        $('#Vm_TallerId').empty();
    //        $("#Vm_TallerId").prepend("<option value='' selected='selected'>Taller</option>");
    //        $(talleres).each(function () {
    //            $("#Vm_TallerId").append($("<option></option>").val(this.id).html(this.nombre));
    //        });
    //    });
    //});

});

function performBusqueda(patente) {
    $.get(`/vehiculos/api/vehiculos/Buscar/${patente}`).done(function (data) {
        if (data.patente && data.bruto) {
            $('#Vm_MarcaId').empty();
            $("#Vm_MarcaId").prepend(`<option value='${data.marcaDesc}' selected='selected'>${data.marcaDesc}</option>`);
            $('#Vm_MarcaId').prop('disabled', 'disabled');


            $('#Vm_ModeloId').empty();
            $("#Vm_ModeloId").prepend(`<option value='${data.modeloDesc}' selected='selected'>${data.modeloDesc}</option>`);
            $('#Vm_ModeloId').prop('disabled', 'disabled');

            $('#Vm_TipoVehiculo').empty();
            $("#Vm_TipoVehiculo").prepend(`<option value='${data.tipoVehiculo}' selected='selected'>${data.tipoVehiculo}</option>`);
            $('#Vm_TipoVehiculo').prop('disabled', 'disabled');

            $('#Vm_Anio').val(data.anio);
            $('#Vm_Anio').prop('disabled', 'disabled');

            $('#Vm_VersionId').empty();
            $("#Vm_VersionId").prepend(`<option value='' selected='selected'>SIN VERSION</option>`);
            $('#Vm_VersionId').prop('disabled', 'disabled');

        } 
    });

}

function handleFiltrosNuevos() {


    $('#Vm_FnTipoVehiculo').change(function () {
        var tipoVehiculoSeleccinado = $("#Vm_FnTipoVehiculo").val();
        if (tipoVehiculoSeleccinado)
            $.get(`/Nuevos/api/TiposVehiculos/${tipoVehiculoSeleccinado}/Marcas`).done(function (modelos) {
                reiniciarControlesNuevo(true, true, true);
                $(modelos).each(function () {
                    $("#Vm_FnMarcaId").append($("<option></option>").val(this.valor).html(this.texto));
                });
            });
        else
            reiniciarControlesNuevo(true, true, true);
    });

    $('#Vm_FnMarcaId').change(function () {
        var marcaSeleccionadaId = $("#Vm_FnMarcaId").val();
        var tipoVehiculoSeleccinado = $("#Vm_FnTipoVehiculo").val();

        if (marcaSeleccionadaId) {
            var urlApiModelos = tipoVehiculoSeleccinado
                ? `/Nuevos/api/TiposVehiculos/${tipoVehiculoSeleccinado}/Marcas/${marcaSeleccionadaId}/Modelos`
                : `/Nuevos/api/Marcas/${marcaSeleccionadaId}/Modelos`;
            $.get(urlApiModelos).done(function (modelos) {
                reiniciarControlesNuevo(false, true, true);
                $(modelos).each(function () {
                    $("#Vm_FnModeloId").append($("<option></option>").val(this.valor).html(this.texto));
                });
            });
        }
        else
            reiniciarControlesNuevo(false, true, true);

    });

    $('#Vm_FnModeloId').change(function () {
        var marcaSeleccionadaId = $("#Vm_FnMarcaId").val();
        var tipoVehiculoSeleccinado = $("#Vm_FnTipoVehiculo").val();
        var modeloIdSeleccionado = $("#Vm_FnModeloId").val();

        if (modeloIdSeleccionado) {
            var urlApiVersiones = tipoVehiculoSeleccinado ?
                `/Nuevos/api/TiposVehiculos/${tipoVehiculoSeleccinado}/Marcas/${marcaSeleccionadaId}/Modelos/${modeloIdSeleccionado}/versiones` :
                `/Nuevos/api/Marcas/${marcaSeleccionadaId}/Modelos/${modeloIdSeleccionado}/versiones`;
            $.get(urlApiVersiones).done(function (modelos) {
                reiniciarControlesNuevo(false, false, true);
                $(modelos).each(function () {
                    $("#Vm_FnVersionId").append($("<option></option>").val(this.valor).html(this.texto));
                });
            });
        }
        else
            reiniciarControlesNuevo(false, false, true);
    });
}

function reiniciarControlesNuevo(marca, modelo, versiona) {
    if (marca) {
        $('#Vm_FnMarcaId').empty();
        $("#Vm_FnMarcaId").prepend("<option value='' selected='selected'>Marca</option>");
    }
    if (modelo) {
        $('#Vm_FnModeloId').empty();
        $("#Vm_FnModeloId").prepend("<option value='' selected='selected'>Modelo</option>");
    }
    if (versiona) {
        $('#Vm_FnVersionId').empty();
        $("#Vm_FnVersionId").prepend("<option value='' selected='selected'>Version</option>");
    }
}