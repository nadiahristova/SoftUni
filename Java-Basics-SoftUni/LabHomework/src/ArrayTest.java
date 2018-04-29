//package ArrayTest;

import java.util.Scanner;

public class ArrayTest {
	public static void main(String[] args) {
		@SuppressWarnings("resource")
		Scanner scn = new Scanner(System.in);		
		
		int sizeOfArray = scn.nextInt();
		//scn.nextLine();
		long[] array = new long[sizeOfArray];
		
		for (int i = 0; i < array.length; i++) {
			array[i] = scn.nextLong(); 
		}
		
		scn.nextLine();
		String line = scn.nextLine();//changed from String command = scn.next()
		
		while (!line.equals("stop")) {
			String[] stringParams = line.trim().split(" ");
			String command = stringParams[0];
			int[] params = new int[2];
			if (!command.equals("lshift") && !command.equals("rshift")) {					
				params[0] = Integer.parseInt(stringParams[1]);
				params[1] = Integer.parseInt(stringParams[2]);
			}		
			performAction(array, command, params);
			printArray(array);
			System.out.print('\n');			
			line = scn.nextLine();//changed from command = scn.next()			
		}		
	}
	
	static void performAction(long[] array, String action, int[] params){	
		int pos = params[0]-1;//subtracted 1
		int value = params[1];

		switch (action) {
		case "multiply":
			array[pos] *= value;
			break;
		case "add":
			array[pos] += value;
			break;
		case "subtract":
			array[pos] -= value;
			break;
		case "lshift":
			arrayShiftLeft(array);
			break;
		case "rshift":
			arrayShiftRight(array);
			break;
		}		
	}

	private static void arrayShiftRight(long[] array) {
		long[] arr = array.clone();// added line
		for (int i = array.length-1; i > 0 ; i--) {
			array[i] = arr[i - 1];			
		}
		array[0] = arr[array.length-1];//added line manually for the element with index 0
	}

	private static void arrayShiftLeft(long[] array) {
		long[] arr = array.clone();//added line 
		for (int i = 0; i < array.length - 1; i++) {
			array[i] = arr[i+1];
		}
		array[array.length-1] = arr[0];//added line manually for the element with last index
	}

	private static void printArray(long[] array) {
		for (int i = 0; i < array.length; i++) {
			System.out.print(array[i] + " ");
		}
	}
}