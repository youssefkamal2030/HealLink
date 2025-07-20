import 'package:flutter/material.dart';
import 'package:heal_link/core/utils/function/app_colors.dart';
import 'package:heal_link/core/widgets/custom_app_bar_pop_widget.dart';
import 'package:heal_link/core/widgets/custom_empty_button.dart';
import 'package:heal_link/core/widgets/custom_full_button.dart';

import '../../../../../generated/l10n.dart';

class PrescriptionView extends StatelessWidget {
  const PrescriptionView({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: SingleChildScrollView(
        child: Padding(
          padding: EdgeInsets.symmetric(horizontal: 16.0),
          child: Column(
            spacing: 24,
            children: [
              CustomAppBarPopWidget(text: S.of(context).prescriptions),
              Image.asset(
                'assets/images/prescription.png',
                height: 476,
                width: 307,
                fit: BoxFit.fill,
              ),
              Row(
                children: [
                  Expanded(
                    child: CustomEmptyButton(
                      text: S.of(context).reject,
                      response: () {},
                      borderColor: AppColors.kErrorColor,
                      borderRadius: 8,
                      height: 40,
                    ),
                  ),
                  SizedBox(width: 14),
                  Expanded(
                    child: CustomFullButton(
                      text: S.of(context).accept,
                      onPressed: () {},
                      radios: 8,
                      height: 40,
                    ),
                  ),
                ],
              ),
            ],
          ),
        ),
      ),
    );
  }
}
