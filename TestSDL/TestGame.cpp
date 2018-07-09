#include "TestGame.h"


namespace TestSDL
{
	TestGame::TestGame()
	{
	}


	TestGame::~TestGame()
	{
		if (_imageSurface != nullptr)
		{
			SDL_FreeSurface(_imageSurface);
			_imageSurface = nullptr;
		}
	}

	void TestGame::Initialize()
	{
	}

	void TestGame::LoadMedia()
	{
		_imageSurface = SDL_LoadBMP("hello_world.bmp");

		if (_imageSurface == nullptr)
		{
			throw std::exception("SDL BMP Load Error");
		}

		SDL_BlitSurface(_imageSurface, nullptr, GetWindowSurface(), nullptr);
		SDL_UpdateWindowSurface(GetWindow());
	}

	void TestGame::Update()
	{
		auto state = GetKeyboardState();

		if (state.KeyDown(SDLK_ESCAPE))
		{
			Exit();
		}
	}

	void TestGame::Draw()
	{
	}

	void TestGame::OnEvent(SDL_Event e)
	{
		if (e.type == SDL_QUIT)
		{
			Exit();
		}
	}
}
