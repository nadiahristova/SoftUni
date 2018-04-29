<?php

namespace SoftUni\FMK\Routers;

interface IRouter {
    /**
     * @return 'package/controller/method/param[0]/param[1]
     */
    public function getURI();

    public function getPost();

    public function getRequestMethod();
}