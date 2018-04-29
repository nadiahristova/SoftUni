

import java.util.Scanner;

public class P2_InstructionSet {
    
    public static void main(String[] args) {
        Scanner input = new Scanner(System.in);
        String opCode = " ";
        
        while (!opCode.equals("END")) {
        	opCode = input.nextLine();
            String[] codeArgs = opCode.split(" ");

            long result = 0;
            switch (codeArgs[0]) {
                case "INC": {
                    long operandOne = Integer.parseInt(codeArgs[1]);
                    result = ++operandOne;
                	System.out.println(result);                    
                    break;
                }
                case "DEC": {
                    long operandOne = Integer.parseInt(codeArgs[1]);
                    result = --operandOne;
                	System.out.println(result);                    
                    break;
                }
                case "ADD": {
                    long operandOne  = Integer.parseInt(codeArgs[1]);
                    long operandTwo = Integer.parseInt(codeArgs[2]);
                    result = operandOne + operandTwo;
                	System.out.println(result);
                    break;
                }
                case "MLA": {
                    long operandOne  = Integer.parseInt(codeArgs[1]);
                    long operandTwo = Integer.parseInt(codeArgs[2]);
                    result = (operandOne * operandTwo);
                	System.out.println(result);
                    break;
                }
                default:{
                    break;
                }
            }
          }
        input.close();
    }
}

