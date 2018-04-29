import java.util.Scanner;
public class _2_InstructionSet {

	public static void main(String[] args) {	    
		Scanner input = new Scanner(System.in);
        
		while (true) {
			String opCode = input.nextLine();
			if (opCode.equals("END")){
				break;
			}
			else{
				String[] codeArgs = opCode.split(" ");
				long result = 0;
				switch (codeArgs[0]) {
				case "INC": {
					int operandOne = Integer.parseInt(codeArgs[1]);
					result = (long)operandOne+1;
					break;
				}
				case "DEC": {
					int operandOne = Integer.parseInt(codeArgs[1]);
					result = (long)operandOne-1;
					break;
				}
				case "ADD": {
					int operandOne  = Integer.parseInt(codeArgs[1]);
					int operandTwo = Integer.parseInt(codeArgs[2]);
					result = (long)operandOne + (long)operandTwo;
					break;
				}
				case "MLA": {
					int operandOne  = Integer.parseInt(codeArgs[1]);
					int operandTwo = Integer.parseInt(codeArgs[2]);
					result = (long)operandOne * (long)operandTwo;
					break;
				}
				default:
					break;
				}
				
				System.out.println(result);
			}
		}
	}
}