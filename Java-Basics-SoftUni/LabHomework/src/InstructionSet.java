//package InstructionSet;

import java.util.Scanner;

public class InstructionSet {
    
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);
        String opCode = input.nextLine();
        
        while (!opCode.equals("END")) {
            String[] codeArgs = opCode.split(" ");

            long result = 0;
            switch (codeArgs[0]) {
                case "INC": {
                    long operandOne = Integer.parseInt(codeArgs[1]);//changed from int to long
                    //result = operandOne++;
                    operandOne++;
                    result = operandOne;
                    //we have a problem with the result here, because in the statement result = operandOne++;
                    //the sequence of the operations is as follows: first we assign the value of the variable
                    //result to operandOne(the value of the oprenandOne is 0 atm), then we 
                    //increment the value of the variable oprenandOne with one = > result=0,operandOne =1
                    //we get wrong output
                    break;
                }
                case "DEC": {
                    long operandOne = Integer.parseInt(codeArgs[1]);//changed from int to long
                    //result = --operandOne;
                    //Here analogically to INC case, we fashion our core: first we decrease the
                    //value of operandOne and then we assign result to operandOne
                    operandOne--;
                    result = operandOne;
                    break;
                }
                case "ADD": {
                    long operandOne  = Integer.parseInt(codeArgs[1]);//changed from int to long
                    long operandTwo = Integer.parseInt(codeArgs[2]);//changed from int to long
                    result = operandOne + operandTwo;
                    break;
                }
                case "MLA": {
                    long operandOne  = Integer.parseInt(codeArgs[1]);//changed from int to long
                    long operandTwo = Integer.parseInt(codeArgs[2]);//changed from int to long
                    result = (long)(operandOne) * operandTwo;
                    //result = (long)(operandOne * operandTwo); - in this example we r trying to multiply
                    //2 numbers that can be represented by the type Integer
                    //Their sum, however, is a number from type long. When one multiplies two integers the 
                    //result is also integer. After we get the wrong number due to loss of accuracy.
                    //Wee need at least 1 of the numbers to be from type long 
                    break;
                }
                default: break;
            }
            System.out.println(result);
            //the program "falls asleep" because we don't actually change the initial input
            //To fix this problem we need, after each cycle in the while loop, to assign new 
            //value to the variable opCode
            opCode = input.nextLine(); // - in other words we need this line
        }
    }
}