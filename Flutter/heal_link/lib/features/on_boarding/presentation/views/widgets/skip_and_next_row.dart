import 'package:flutter/material.dart';
import 'package:go_router/go_router.dart';
import 'package:heal_link/core/utils/app_styles.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';

import '../../../../../core/utils/app_router.dart';
import '../../../../../core/utils/constant.dart';
import '../../../../../generated/l10n.dart';

class SkipAndNextRow extends StatelessWidget {
  const SkipAndNextRow({
    super.key,
    required this.pageController,
    required this.index,
    required this.onNextChanged,
  });

  final PageController pageController;
  final int index;
  final void Function() onNextChanged;

  @override
  Widget build(BuildContext context) {
    return Positioned(
      top: AppConstant.height * .86,
      child: SizedBox(
        width: AppConstant.width,
        child: Padding(
          padding: const EdgeInsets.symmetric(horizontal: 28.0),
          child: Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              TextButton(
                child: Text(
                  S.of(context).skip,
                  style: AppTextStyles.popins400style14LightBlackColor.copyWith(
                    fontSize: 16,
                    color: AppColors.kPrimaryColor,
                  ),
                ),
                onPressed: () {
                  context.go(AppRouter.userTypeScreen);
                },
              ),
              TextButton(
                onPressed: onNextChanged,
                child: Text(
                  S.of(context).next,
                  style: AppTextStyles.popins400style14LightBlackColor.copyWith(
                    fontSize: 16,
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
