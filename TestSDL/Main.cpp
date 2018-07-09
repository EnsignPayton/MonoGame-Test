#include <iostream>
#include "TestGame.h"

int main(int argc, char *argv[])
{
	try
	{
		auto game = TestSDL::TestGame();

		game.Run();
	}
	catch (const std::exception& ex)
	{
		std::cerr << ex.what() << std::endl;
	}

	return 0;
}