import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_router.dart';
import 'package:heal_link/features/on_boarding/presentation/views/widgets/on_boarding_page1.dart';
import 'package:heal_link/features/on_boarding/presentation/views/widgets/on_boarding_page2.dart';
import 'package:heal_link/features/on_boarding/presentation/views/user_type_screen.dart';
import 'package:heal_link/features/on_boarding/presentation/views/widgets/skip_and_next_row.dart';
import 'custom_dots_indicator.dart';

class OnBoardingViewBody extends StatefulWidget {
  const OnBoardingViewBody({super.key});

  @override
  State<OnBoardingViewBody> createState() => _OnBoardingViewBodyState();
}

class _OnBoardingViewBodyState extends State<OnBoardingViewBody> {
  int index = 0;
  static const List<Widget> pages = [
    OnBoardingPage1(),
    OnBoardingPage2(),
    UserTypeScreen(),
  ];
  var pageController = PageController();

  @override
  Widget build(BuildContext context) {
    return Stack(
      children: [
        PageView.builder(
          controller: pageController,
          itemBuilder: (context, index) {
            return pages[index];
          },
          onPageChanged: (value) {
            if (value == 2) {
              context.go(AppRouter.userTypeScreen);
            }
            index = value;
            setState(() {});
          },
          itemCount: pages.length,
        ),
        CustomDotsIndicator(index: index),
        SkipAndNextRow(
          pageController: pageController,
          index: index,
          onNextChanged: () {
            if (index == 1) {
              context.go(AppRouter.userTypeScreen);
            }
            index++;
            pageController.nextPage(
              duration: const Duration(milliseconds: 300),
              curve: Curves.easeInOut,
            );
            setState(() {});
          },
        ),
      ],
    );
  }
}
