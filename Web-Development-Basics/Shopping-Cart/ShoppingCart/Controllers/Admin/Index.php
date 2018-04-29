<?php

namespace Controllers\Admin;


use SoftUni\FMK\DefaultController;

class Index extends DefaultController {
    public function test(){
        $val = new \SoftUni\FMK\Validator();
        $val->setRule('minlength', 'sisi', 3);
        var_dump($val->validate());
        print_r($val->getErrors());
        $view = \SoftUni\FMK\View::getInstance();
        $view->username = 'nanni-bani';
        $view->appendToLayout('body', 'admin.index');
        $view->appendToLayout('body2', 'index');
        $view->display('layouts.default', ['ala', 'bala']);
        \SoftUni\FMK\InputData::getInstance()->get(0, 'int');
        \SoftUni\FMK\App::getInstance()->displayError(404);
        exit;
    }
}