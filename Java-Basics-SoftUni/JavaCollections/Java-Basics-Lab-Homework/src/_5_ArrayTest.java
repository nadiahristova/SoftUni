
import java.util.Scanner;
public class _5_ArrayTest {
	public static void main(String[] args) {
		@SuppressWarnings("resource")
		Scanner scn = new Scanner(System.in);	
		int sizeOfArray = scn.nextInt();
		long[] array = new long[sizeOfArray];
		
		for (int i = 0; i < array.length; i++) {
			array[i] = scn.nextLong(); 
		}		
		String command = scn.next();

		while (!command.equals("stop")) {
			String line = scn.nextLine().trim();
			int[] params = new int[2];

			if(command.equals("add") || command.equals("subtract") || command.equals("multiply")) {
				String[] stringParams = line.split(" ");
				params[0] = Integer.parseInt(stringParams[0]);
				params[1] = Integer.parseInt(stringParams[1]);

				array=performAction(array, command, params);
			}
			else{
				array=performAction(array, command, params);
			}
			
			printArray(array);
			System.out.print('\n');
			
			command = scn.next();			
		}		
	}	
	static long[] performAction(long[] arr, String action, int[] params){
		long[] array = arr.clone();
		int pos = params[0];
		int value = params[1];

		switch (action) {
		case "multiply":
			array[pos-1] *= value;
			break;
		case "add":
			array[pos-1] += value;
			break;
		case "subtract":
			array[pos-1] -= value;
			break;
		case "lshift":
			arrayShiftLeft(array);
			break;
		case "rshift":
			arrayShiftRight(array);
			break;
		}
		 return array;
	}

	private static void arrayShiftRight(long[] array) {
		long a=array[array.length-1];
		for (int i = array.length-1; i >= 1 ; i--) {		    
			array[i] = array[i - 1];
		}
		array[0]=a;
	}

	private static void arrayShiftLeft(long[] array) {
		long a=array[0];
		for (int i = 0; i < array.length - 1; i++) {
			array[i] = array[i+1];
		}
		array[array.length-1]=a;
	}

	private static void printArray(long[] array) {
		for (int i = 0; i < array.length; i++) {
			System.out.print(array[i] + " ");
		}
	}
}