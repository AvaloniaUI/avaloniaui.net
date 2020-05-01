$(function () {
    'use strict';

    $('pre code').each(function (i, block) {
        hljs.highlightBlock(block);
    }); 

    anchors.add();

    var navbar = $('#navbar');
    var scrollListeners = $('.scroll-listener');

    // Window scroll listeners
    $(window).scroll(function () {
        if (navbar.offset().top > 10) {
            scrollListeners.addClass('scrolled');
        } else {
            scrollListeners.removeClass('scrolled');
        }
    });

    // Hamburger menu
    $('#toplevel-menu-toggle').click(function () {
        navbar.toggleClass('open');
    });

    $('body').click(function () {
        navbar.removeClass('open');
    });

    navbar.click(function (e) {
        e.stopPropagation();
    });

    // Tree view
    $('.expander').click(function () {
        $(this).parent().toggleClass('expanded');
    });

    // Carousel.
    $('.carousel').slick({
        dots: true,
        arrows: false
    });

    // Lightbox
    $('.lightbox').magnificPopup({ type: 'image' });
})
