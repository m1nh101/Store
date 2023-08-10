import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobile/presentation/home/home_bloc.dart';
import 'package:mobile/presentation/home/home_event.dart';
import 'package:mobile/presentation/home/home_state.dart';

class Home extends StatefulWidget {
  const Home({super.key});

  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  final HomeBloc homeBloc = HomeBloc();

  @override
  void initState() {
    homeBloc.add(HomeInitialEvent());
  }

  @override
  Widget build(BuildContext context) {

    return Scaffold(
      appBar: appBar(),
    );

    // return BlocConsumer<HomeBloc, HomeState>(
    //   bloc: homeBloc,
    //   listenWhen: (previous, current) => current is HomeActionState,
    //   buildWhen: (previous, current) => current !is HomeActionState,
    //   listener: (BuildContext context, HomeState state) {  },
    //   builder: (BuildContext context, HomeState state) {
    //     switch(state.runtimeType) {
    //
    //     }
    //   },
    // );

  }

  AppBar appBar() {
    return AppBar(
      centerTitle: false,
      title: const Text("Store"),
      actions: [
        IconButton(onPressed: () {}, icon: const Icon(Icons.search)),
        IconButton(onPressed: () {}, icon: const Icon(Icons.shopping_bag)),
        IconButton(onPressed: () {}, icon: const Icon(Icons.account_circle))
      ],
    );
  }
}
