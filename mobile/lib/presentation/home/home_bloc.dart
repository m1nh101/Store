import 'dart:async';

import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/presentation/home/home_event.dart';
import 'package:mobile/presentation/home/home_state.dart';

class HomeBloc extends Bloc<HomeEvent, HomeState> {
  HomeBloc() : super (HomeStateInitial()) {
    on<HomeInitialEvent>(homeInitialEvent);
    on<HomeNavigateUserEvent>(homeNavigateUserEvent);
    on<HomeNavigateCartEvent>(homeNavigateCartEvent);
    on<HomeProductClickedEvent>(homeProductClickedEvent);
    
  }

  FutureOr<void> homeInitialEvent(HomeInitialEvent event, Emitter<HomeState> emit) {
  }

  FutureOr<void> homeNavigateUserEvent(HomeNavigateUserEvent event, Emitter<HomeState> emit) {
  }

  FutureOr<void> homeNavigateCartEvent(HomeNavigateCartEvent event, Emitter<HomeState> emit) {
  }

  FutureOr<void> homeProductClickedEvent(HomeProductClickedEvent event, Emitter<HomeState> emit) {
  }
}