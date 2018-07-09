#pragma once
#include "Game.h"

namespace TestSDL
{
	class TestGame final : public Game
	{
	public:
		TestGame();
		~TestGame();

	private:
		void Initialize();
		void LoadMedia();
		void Update();
		void Draw();
		void OnEvent(SDL_Event e);

		SDL_Surface* _imageSurface;
	};
}

