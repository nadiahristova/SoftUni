<?php

namespace PFCS\FMK\Annotation;

abstract class Annotation
{
    const FOR_CLASS = 1;
    const FOR_METHOD = 2;
    const FOR_ALL = 3;
    const FOR_PROPERTY = 4;

    /**
     * Value property. Common among all derived classes.
     *
     * @var string
     */
    public $value;

    /**
     * Constructor.
     *
     * @param array $data Key-value for properties to be defined in this class.
     */
    public function __construct(array $data)
    {
        foreach ($data as $key => $value) {
            $this->$key = $value;
        }
    }

    /**
     * Returns a string representation of the intended usage of an annotation.
     */
    abstract public function usage() : string;

    /**
     * Returns an integer representation of the type(s) of PHP language constructs
     * the annotation is applicable to. Use the Annotation::FOR_* consts to return
     * the desired result.
     *
     * Examples:
     *  // Only valid on classes
     *  function isFor() { return Annotation::FOR_CLASS; }
     *
     *  // Valid on methods or properties
     *  function isFor() { return Annotation::FOR_METHOD | Annotation::FOR_PROPERTY; }
     */
    abstract public function isFor() : int;

    /**
     * Error handler for unknown property accessor in Annotation class.
     *
     * @param string $name Unknown property name.
     *
     * @throws \BadMethodCallException
     */
    public function __get($name)
    {
        throw new \BadMethodCallException(
            sprintf("Unknown property '%s' on annotation '%s'.", $name, get_class($this))
        );
    }

    /**
     * Error handler for unknown property mutator in Annotation class.
     *
     * @param string $name  Unknown property name.
     * @param mixed  $value Property value.
     *
     * @throws \BadMethodCallException
     */
    public function __set($name, $value)
    {
        throw new \BadMethodCallException(
            sprintf("Unknown property '%s' on annotation '%s'.", $name, get_class($this))
        );
    }
}