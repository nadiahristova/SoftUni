<?php

namespace PFCS\FMK\Routers;

interface IRouter
{
    public function getURI();

    public function getPost();

    public function getRequestMethod();
}