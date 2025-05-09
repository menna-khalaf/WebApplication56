﻿
html {
    font - size: 14px;
}

@media(min - width: 768px) {
    html {
        font - size: 16px;
    }
}

html {
    position: relative;
    min - height: 100 %;
}

body {
    margin - bottom: 60px;
}

body {
    font - family: "Open Sans", sans - serif;
    color: #444444;
}

a {
    color: #47b2e4;
    text - decoration: none;
}

a:hover {
    color: #73c5eb;
    text - decoration: none;
}


h1,
    h2,
    h3,
    h4,
    h5,
    h6 {
    font - family: "Jost", sans - serif;
}

/*--------------------------------------------------------------
# Preloader
--------------------------------------------------------------*/
#preloader {
    position: fixed;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z - index: 9999;
    overflow: hidden;
    background: #37517e;
}

#preloader:before {
    content: "";
    position: fixed;
    top: calc(50 % - 30px);
    left: calc(50 % - 30px);
    border: 6px solid #37517e;
    border - top - color: #fff;
    border - bottom - color: #fff;
    border - radius: 50 %;
    width: 60px;
    height: 60px;
    animation: animate - preloader 1s linear infinite;
}

@keyframes animate - preloader {
    0 % {
        transform: rotate(0deg);
    }

    100 % {
        transform: rotate(360deg);
    }
}

/*--------------------------------------------------------------
# Back to top button
--------------------------------------------------------------*/
.back - to - top {
    position: fixed;
    visibility: hidden;
    opacity: 0;
    right: 15px;
    bottom: 15px;
    z - index: 996;
    background: #47b2e4;
    width: 40px;
    height: 40px;
    border - radius: 50px;
    transition: all 0.4s;
}

    .back - to - top i {
    font - size: 24px;
    color: #fff;
    line - height: 0;
}

    .back - to - top:hover {
    background: #6bc1e9;
    color: #fff;
}

    .back - to - top.active {
    visibility: visible;
    opacity: 1;
}

/*--------------------------------------------------------------
# Header
--------------------------------------------------------------*/
#header {
    transition: all 0.5s;
    z - index: 997;
    padding: 15px 0;
}

#header.header - scrolled,
    #header.header - inner - pages {
    background: rgba(40, 58, 90, 0.9);
}

#header.logo {
    font - size: 30px;
    margin: 0;
    padding: 0;
    line - height: 1;
    font - weight: 500;
    letter - spacing: 2px;
    text - transform: uppercase;
}

#header.logo a {
    color: #fff;
}

#header.logo img {
    max - height: 40px;
}

/*--------------------------------------------------------------
# Hero Section
--------------------------------------------------------------*/
#hero {
    width: 100 %;
    height: 80vh;
    background: #37517e;
}

#hero.container {
    padding - top: 72px;
}

#hero h1 {
    margin: 0 0 10px 0;
    font - size: 48px;
    font - weight: 700;
    line - height: 56px;
    color: #fff;
}

#hero h2 {
    color: rgba(255, 255, 255, 0.6);
    margin - bottom: 50px;
    font - size: 24px;
}

#hero.btn - get - started {
    font - family: "Jost", sans - serif;
    font - weight: 500;
    font - size: 16px;
    letter - spacing: 1px;
    display: inline - block;
    padding: 10px 28px 11px 28px;
    border - radius: 50px;
    transition: 0.5s;
    margin: 10px 0 0 0;
    color: #fff;
    background: #47b2e4;
}

#hero.btn - get - started:hover {
    background: #209dd8;
}

#hero.btn - watch - video {
    font - size: 16px;
    display: flex;
    align - items: center;
    transition: 0.5s;
    margin: 10px 0 0 25px;
    color: #fff;
    line - height: 1;
}

#hero.btn - watch - video i {
    line - height: 0;
    color: #fff;
    font - size: 32px;
    transition: 0.3s;
    margin - right: 8px;
}

#hero.btn - watch - video:hover i {
    color: #47b2e4;
}

#hero.animated {
    animation: up - down 2s ease -in -out infinite alternate - reverse both;
}

@media(max - width: 991px) {
    #hero {
        height: 100vh;
        text - align: center;
    }

    #hero.animated {
        animation: none;
    }

    #hero.hero - img {
        text - align: center;
    }

    #hero.hero - img img {
        width: 50 %;
    }
}

@media(max - width: 768px) {
    #hero h1 {
        font - size: 28px;
        line - height: 36px;
    }

    #hero h2 {
        font - size: 18px;
        line - height: 24px;
        margin - bottom: 30px;
    }

    #hero.hero - img img {
        width: 70 %;
    }
}

@media(max - width: 575px) {
    #hero.hero - img img {
        width: 80 %;
    }

    #hero.btn - get - started {
        font - size: 16px;
        padding: 10px 24px 11px 24px;
    }
}

@keyframes up - down {
    0 % {
        transform: translateY(10px);
    }

    100 % {
        transform: translateY(-10px);
    }
}
