<?php

namespace PFCS\FMK;


class BaseController
{
    /**
     * @var  \PFCS\FMK\App
     */
    public $app;
    /**
     * @var \PFCS\FMK\View
     */
    public $view;
    /**
     * @var \PFCS\FMK\Config
     */
    public $config;
    /**
     * @var \PFCS\FMK\InputData
     */
    public $input;
//    /**
//     * @var \PFCS\FMK\Session\ISession
//     */
//    public $session;

    public function __construct()
    {
        $this->app = \PFCS\FMK\App::getInstance();
        $this->view = \PFCS\FMK\View::getInstance();
        $this->config = \PFCS\FMK\Config::getInstance();
        $this->input = \PFCS\FMK\InputData::getInstance();
//        $this->session = $this->app->getSession(); we need Http Context maybe :P
//        View::logged((bool)$this->session->userId);
//        View::role($this->session->user['role']);
    }

    public function jsonResponse() // ???
    {
    }

    public function redirect($url) { // ???
        header('Location: '  . $url);
    }

//    public function isAdmin() Http Context
//    {
//        if($this->session->user['role'] == 3) {
//            return true;
//        }
//        return false;
//    }
//
//    public function isEditor()
//    {
//        if($this->session->user['role'] == 2) {
//            return true;
//        }
//        return false;
//    }
}