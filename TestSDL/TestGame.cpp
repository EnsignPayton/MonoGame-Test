#include "TestGame.h"
#include <SDL.h>

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
		SDL_Surface* rawSurface = SDL_LoadBMP("hello_world.bmp");

		if (rawSurface == nullptr)
		{
			throw std::exception("SDL BMP Load Error");
		}

		_imageSurface = SDL_ConvertSurface(rawSurface, GetWindowSurface()->format, 0);

		SDL_FreeSurface(rawSurface);

		if (_imageSurface == nullptr)
		{
			throw std::exception("SDL Image Convert Error");
		}
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
		SDL_FillRect(GetWindowSurface(), nullptr, 0);

		SDL_Rect destination =
		{
			0, 0, GetWindowWidth(), GetWindowHeight()
		};

		SDL_BlitScaled(_imageSurface, nullptr, GetWindowSurface(), &destination);

		SDL_UpdateWindowSurface(GetWindow());
	}

	void TestGame::OnEvent(SDL_Event e)
	{
		if (e.type == SDL_QUIT)
		{
			Exit();
		}
	}
}
