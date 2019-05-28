/// <reference path="jquery-2.0.0.intellisense.js" />
/// <reference path="jquery-2.0.0.min.js" />

$(document).ready(function () {
    $('.owl-homebanner').owlCarousel({
        loop: false,
        lazyLoad: true,
        autoplay: true,
        autoplayTimeout:8000,
        margin: 0,
        rewind:true,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: false,
        responsive: {
            0: {
                items: 1
            }
        }
    })
    $('.owl-hotnews').owlCarousel({
        loop: false,
        lazyLoad: true,
        navContainer: '#hotNewsNav',
        autoplay: false,
        margin: 15,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    })
    $('.owl-projectImage').owlCarousel({
        loop: false,
        lazyLoad: true,
        navContainer: '#projectImageNav',
        autoplay: false,
        margin: 15,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 4
            }
        }
    })
    $('.owl-projectVideo').owlCarousel({
        loop: false,
        lazyLoad: true,
        navContainer: '#projectVideoNav',
        autoplay: false,
        margin: 15,
        nav: true,
        navText: ["<i class='fa fa-angle-left'></i>", "<i class='fa fa-angle-right'></i>"],
        dots: false,
        responsive: {
            0: {
                items: 1
            },
            600: {
                items: 2
            },
            1000: {
                items: 3
            }
        }
    })
});
