<?php

namespace PFCS\FMK\Routers;


class DefaultRouter implements IRouter
{
//    private $_controller = null;
//    private $_method = null;
//    private $_params = array();

    public function getURI() : string
    {
        return $_GET['uri'];
    }

    public function getPost()
    {
        return $_POST;
    }

    public function getRequestMethod() : string
    {
        return $_SERVER['REQUEST_METHOD'];
    }
}